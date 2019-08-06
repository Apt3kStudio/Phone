using Phone.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phone.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[HotReloader.CSharpVisual]
    public partial class AddDevice : ContentPage
    {
        private const int AnimationDuration = 200;
        public string icon { get; set; }
        public AddDevice()
        {
            icon = FontAwesomeIcons.BatteryQuarter;
            InitializeComponent();
            SizeChanged += LoginPage_SizeChanged;
            // LayoutChanged += LoginPage_LayoutChanged;

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

            SelectorButon.TranslateTo(index * view.Width, 0, AnimationDuration, Easing.CubicInOut).FireAndForget();
            await SelectorButtonLabel.FadeTo(0, AnimationDuration / 2);
            SelectorButtonLabel.Text = index == 1 ? "New" : "Existing";
            await SelectorButtonLabel.FadeTo(1, AnimationDuration / 2);
           
            var revealRegScreen = index == 0 ? RegisteredDeviceScreen : DeviceRegistrationScreen;
            var hideScreen = revealRegScreen == RegisteredDeviceScreen ? DeviceRegistrationScreen : RegisteredDeviceScreen;

            var direction = revealRegScreen == RegisteredDeviceScreen ? -1 : 1;


            await Task.WhenAll(revealRegScreen.TranslateTo(direction * 200, 0, AnimationDuration, Easing.SinOut), hideScreen.FadeTo(0, AnimationDuration));
            hideScreen.IsVisible = false;

            revealRegScreen.TranslationX = -direction * 200;//Screen start at position x = 0


            revealRegScreen.IsVisible = true;
            await Task.WhenAll(revealRegScreen.TranslateTo(0, 0, AnimationDuration, Easing.SinOut), hideScreen.FadeTo(1, AnimationDuration));


        }
    }
}