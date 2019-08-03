using Android.Content;
using Phone.Services;
using Phone.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            auth = new AuthVM()
            {
                Email = "dioscarr@gmail.com",
                Password = "Password@123"
            };
            BindingContext = auth;
        }
        public Login(string username)
        {
            InitializeComponent();
            auth = new AuthVM()
            {
                Email = username,
                Password = ""
            };
            BindingContext = auth;
        }

        private WebPortalApiServices apiService = new WebPortalApiServices();
        public AuthVM auth { set; get; }
        public string ErrorMessage { get; set; }

        async void NavigateToRegistrationPage_ClickedAsync(object sender, EventArgs e)
        {
            auth.isBussy = true;
           // await Navigation.PushModalAsync(new NavigationPage(new Registration()));            
            auth.isBussy = false;
        }
        void goToStartUpPage(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(auth.Email) && !string.IsNullOrEmpty(auth.Password))
            {
                auth.SigningInCommand.Execute(true);
            }
        }
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            using (SKPaint paint = new SKPaint())
            {
                SKColor SpideyBlueBckGrnd;
                SKColor.TryParse("#020f1f", out SpideyBlueBckGrnd);
                SKColor SpideyLightBlueBckGrnd;
                SKColor.TryParse("#001c41", out SpideyLightBlueBckGrnd);
                paint.Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(info.Rect.Top, info.Rect.Top),
                                    new SKPoint(info.Rect.Bottom, info.Rect.Bottom),
                                    new SKColor[] { SpideyBlueBckGrnd, SpideyLightBlueBckGrnd },
                                    new float[] { 0, 0.60f },
                                    SKShaderTileMode.Mirror);
                canvas.DrawRect(info.Rect, paint);
            }
        }
    }
}