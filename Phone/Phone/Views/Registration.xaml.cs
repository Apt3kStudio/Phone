using Android.Content;
using Phone.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
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
    public partial class Registration : ContentPage
    {

        public Registration()
        {
            InitializeComponent();
            auth = new AuthVM()
            {
                Email = "",
                Password = "",
                ConfirmPassword = "",
                ErrorMessage="Init Error"
            };

            BindingContext = auth;
        }      
        public AuthVM auth { get; set; }      

        public  void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
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

        void BackToLogin(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new Login());            
        }
    }
    
}