using Android.Content;
using Android.Widget;
using Phone.Models;
using Phone.Services;
using Phone.Views;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Phone.ViewModels
{
    public class LoginUserViewModel 
    {       
        private Context _context;
        private WebPortalApiServices _portalApiService;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        public bool isAuthenticated { get; set; }
        SKCanvasView canvasView;
        INavigation Navigation;
        public LoginUserViewModel(INavigation HomePageNav, Context context)
        {
            _portalApiService =new WebPortalApiServices();
            //canvasView = new SKCanvasView(_context);
            //canvasView.PaintSurface += OnCanvasViewPaintSurface;
            _context = context;
            Navigation = HomePageNav;
        }
        public static async System.Threading.Tasks.Task<bool> IsUseregisteredAsync()
        {
            var Email = await SecureStorage.GetAsync("Email");
            
            return (!string.IsNullOrEmpty(Email))?true:false; 
        }
        public Command SigningInCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var message = await _portalApiService.SigningIn(Email, Password);
                    if (message.isSuccess)
                    {
                        isAuthenticated = true;
                        Toast.MakeText(_context, message.MessageText, ToastLength.Long).Show();
                        await SecureStorage.SetAsync("Email", Email);                        
                        await Navigation.PushModalAsync(new NavigationPage(new HomePage(_context)));
                    }
                    else
                    {
                        isAuthenticated = false;
                        Toast.MakeText(_context, message.MessageText, ToastLength.Long).Show();
                    }
                });
            }
            set
            {

            }
        }
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            using (SKPaint paint = new SKPaint())
            {
                //Create 300-pixel square centered recangle
                float x = (info.Width - 300) / 2;
                float y = (info.Height - 300) / 2;
                SKRect rect = new SKRect(x, y, x + 300, y + 300);

                //Create linear gradiwnt from upper-lef to lower right
                paint.Shader = SKShader.CreateLinearGradient(new SKPoint(rect.Left, rect.Top),
                                                               new SKPoint(rect.Right, rect.Bottom),
                                                               new SKColor[] { SKColors.Red, SKColors.Blue },
                                                               new float[] { 0, 1 },
                                                               SKShaderTileMode.Repeat);

                // Draw the gradient on thge rectangle
                canvas.DrawRect(info.Rect, paint);
            }
        }

    }
}
