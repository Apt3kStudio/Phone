using Phone.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Phone.Extensions;
using Phone.ViewModels;
using Phone.Models;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDevice : ContentPage
    {
        public const int AnimationDuration = 200;
        private ConnectedDevicesVM cd;
        private RegisteredDevice regDeviceModel { get; set; }

        public string icon { get; set; }
        public AddDevice()
        {
            InitializeComponent();
            cd= new  ConnectedDevicesVM();
            cd.loadUnregisteredDevices();
            cd.loadRegisteredDevices(false,3);
            regDeviceModel = cd.RegisteredDevices.FirstOrDefault();
            //cd.SelectedUnRegDevic = cd.UnRegisteredDevices.FirstOrDefault();
            BindingContext = cd;
            SizeChanged += LoginPage_SizeChanged;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();          
            
         
        }
        private void LoginPage_SizeChanged(object sender, EventArgs e)
        {
            if (SelectorBackground.Height < 0) return;
            SelectorBackground.CornerRadius = new CornerRadius(SelectorBackground.Height / 2);
            SelectorButon.CornerRadius = (float)(SelectorButon.Height / 2);
        }
        private async void SelectorOption_Tapped(object sender, EventArgs e)
        {
            if (!(sender is View view)) return;
            var index = Grid.GetColumn(view);
            // var nextIndex = index == 0 ? 1 : 0;
            cd.RefreshDevicesColls();
            SelectorButon.TranslateTo(index * view.Width, 0, AnimationDuration, Easing.CubicInOut).FireAndForget();
            await SelectorButtonLabel.FadeTo(0, AnimationDuration / 2);
            SelectorButtonLabel.Text = index == 1 ? "New" : "Existing";
            await SelectorButtonLabel.FadeTo(1, AnimationDuration / 2);

            var revealRegScreen = index == 0 ? RegisteredDeviceScreen : DeviceRegistrationScreen;
            var hideScreen = revealRegScreen == RegisteredDeviceScreen ? DeviceRegistrationScreen : RegisteredDeviceScreen;

            var revealDevices = index == 0 ? RegistedDevices : UnRegistedDevices;
            var hideDevices = revealDevices == RegistedDevices ? UnRegistedDevices : RegistedDevices;



            var direction = revealRegScreen == RegisteredDeviceScreen ? -1 : 1;

           

            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {

                await Task.WhenAll(hideScreen.TranslateTo(direction * 100, 0, AnimationDuration, Easing.SinOut), hideScreen.FadeTo(0, AnimationDuration));
                await Task.WhenAll(hideDevices.TranslateTo(direction * 100, 0, AnimationDuration, Easing.SinOut), hideDevices.FadeTo(0, AnimationDuration));

                hideScreen.IsVisible = false;
                hideDevices.IsVisible = false;


                revealRegScreen.TranslationX = -direction * 100;//Screen start at position x = 0
                revealDevices.TranslationX = -direction * 100;

                revealRegScreen.IsVisible = true;
                revealDevices.IsVisible = true;
                await Task.WhenAll(revealRegScreen.TranslateTo(0, 0, AnimationDuration, Easing.SinOut), revealRegScreen.FadeTo(1, AnimationDuration));
                await Task.WhenAll(revealDevices.TranslateTo(0, 0, AnimationDuration, Easing.SinOut), revealDevices.FadeTo(1, AnimationDuration));

            });

        }


        private async void SelectedDevice_Tap(object sender, EventArgs e)
        {
            if (!(sender is View view)) return;
        }
        private void BackgroundGradient_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            // get the brush based on the theme
            SKColor gradientStart;
            SKColor.TryParse("#373B44", out gradientStart);
            SKColor gradientMid;
            SKColor.TryParse("#355C7D", out gradientMid);
            SKColor gradientEnd;
            SKColor.TryParse("#4286f4", out gradientEnd);

            // gradient background with 3 colors
            SKPaint backgroundBrush = new SKPaint();
            backgroundBrush.Shader = SKShader.CreateRadialGradient(
               new SKPoint(0, info.Height * .8f),
               info.Height * .8f,
               new SKColor[] { gradientStart, gradientMid, gradientEnd },
               new float[] { 0, .5f, 1 },
               SKShaderTileMode.Clamp);

            SKRect backgroundBounds = new SKRect(0, 0, info.Width, info.Height);
            canvas.DrawRect(backgroundBounds, backgroundBrush);
        }
        //Todo Maybe usefull for animation
        void DeviceRegistrationCMD(object sender, EventArgs e)
        {
            if (RegisteredCollView.SelectedItem == null)
                return;
         
            RegisteredDevice reg = (RegisteredDevice)RegisteredCollView.SelectedItem;
            cd.RegisterDevice(reg);
        }
        void unregisterDeviceCMD(object sender, EventArgs e)
        {
            if (RegisteredCollView.SelectedItem == null)
                return;
            RegisteredDevice reg = (RegisteredDevice)RegisteredCollView.SelectedItem;
        }
        void OnRegCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string previous = (e.PreviousSelection.FirstOrDefault() as RegisteredDevice)?.deviceName;
            string current = (e.CurrentSelection.FirstOrDefault() as RegisteredDevice)?.deviceName;
            cd.loadRegisteredDevices();
            
            RegisteredDevice CurrnRegDev = (e.CurrentSelection.FirstOrDefault() as RegisteredDevice);
            lcldeviceName.Text = CurrnRegDev.deviceName;
            lclmanufacturer.Text = CurrnRegDev.manufacturer;
            lclplatform.Text = CurrnRegDev.platform;
            lclidiom.Text = CurrnRegDev.idiom;
            UnRegisteredImage.Source = CurrnRegDev.ImageSource;

        }
        void OnUnRegCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string previous = (e.PreviousSelection.FirstOrDefault() as RegisteredDevice)?.deviceName;
            string current = (e.CurrentSelection.FirstOrDefault() as RegisteredDevice)?.deviceName;
            RegisteredDevice CurrnUnRegDev = (e.CurrentSelection.FirstOrDefault() as RegisteredDevice);

            RegisteredDevice CurrnRegDev = (e.CurrentSelection.FirstOrDefault() as RegisteredDevice);
            lcldeviceName.Text = CurrnRegDev.deviceName;
            lclmanufacturer.Text = CurrnRegDev.manufacturer;
            lclplatform.Text = CurrnRegDev.platform;
            lclidiom.Text = CurrnRegDev.idiom;
            UnRegisteredImage.Source = CurrnRegDev.ImageSource;
        }

    }
}