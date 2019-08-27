using Nest;
using Phone.Models;
using Phone.Utility;
using Phone.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevicesHome : ContentPage
    {     
        public string icon { get; set; }
        public SKCanvasView canvasView;
        public ConnectedDevicesVM cd;

        public DevicesHome()
        {
            InitializeComponent();
            canvasView = new SKCanvasView(Android.App.Application.Context);
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            cd = new ConnectedDevicesVM(true);
         
            BindingContext = cd;
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as RegisteredDevice;
            if (item == null)
                return;

            //   await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
        void AddItem_Clicked(object sender, EventArgs e)
        {
           // await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(async() =>
            {
                cd.RegisteredDevices.Clear();
               await cd.loadRegisteredDevicesAsync(true, 0);
            });

            canvasView.ClearAnimation();
            canvasView.ClearFocus();
            canvasView.Invalidate();
       

        }
        protected void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            using (SKPaint paint = new SKPaint())
            {
                SKColor SpideyBlueBckGrnd;
                //SKColor.TryParse("#020f1f", out SpideyBlueBckGrnd);
                SKColor.TryParse("#ECE9E6", out SpideyBlueBckGrnd);
                SKColor SpideyLightBlueBckGrnd;
                SKColor.TryParse("#FFFFFF", out SpideyLightBlueBckGrnd);
                //SKColor.TryParse("#001c41", out SpideyLightBlueBckGrnd);
                paint.Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(info.Rect.Top, info.Rect.Top),
                                    new SKPoint(info.Rect.Bottom, info.Rect.Bottom),
                                    new SKColor[] { SpideyBlueBckGrnd, SpideyLightBlueBckGrnd },
                                    new float[] { 0, 10.80f },
                                    SKShaderTileMode.Mirror);
                canvas.DrawRect(info.Rect, paint);
            }     
        }
        private void BackgroundGradient_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            // get the brush based on the theme
            SKColor gradientStart;
            SKColor.TryParse("#d6d6d6", out gradientStart);
            SKColor gradientMid;
            SKColor.TryParse("#d8d8e3", out gradientMid);
            SKColor gradientEnd;
            SKColor.TryParse("#cbcbd6", out gradientEnd);

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

    }
}