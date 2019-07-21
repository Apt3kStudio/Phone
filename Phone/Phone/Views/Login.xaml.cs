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
    public partial class Login : ContentPage, INotifyPropertyChanged
    {
        private WebPortalApiServices apiService = new WebPortalApiServices();
        public LoginUserViewModel UserVM { set; get; }
        public string ErrorMessage { get; set; }
        private Context _context;
        public Login(Context context)
        {
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
           
            Content = canvasView;
            _context = context;
            InitializeComponent();
            UserVM = new LoginUserViewModel(Navigation, _context) {
                 //Email = "newuser@app.com",
                 //Password = "Password@123"
            };           
            BindingContext = UserVM;           
        }
        public Login(string username, Context context)
        {
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            
            Content = canvasView;
            _context = context;
            InitializeComponent();
            UserVM = new LoginUserViewModel(Navigation, _context)
            {
                Email = username,
                Password = ""
            };           
            BindingContext = UserVM;              
        }
        async void NavigateToRegistrationPage_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("SignUp", "You will be redirected to the Registration form", "OK");
            await Navigation.PushModalAsync(new NavigationPage(new Registration(_context)));           
        }
        async void goToStartUpPage(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UserVM.Email)&& !string.IsNullOrEmpty(UserVM.Password))
            {
                UserVM.Message = "too soon!";               
                UserVM.SigningInCommand.Execute(null);                
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
                                    new SKPoint(info.Rect.Top,info.Rect.Top),
                                    new SKPoint(info.Rect.Bottom, info.Rect.Bottom),
                                    new SKColor[] { SpideyBlueBckGrnd, SpideyLightBlueBckGrnd },
                                    new float[] { 0, 0.60f },
                                    SKShaderTileMode.Mirror);
                canvas.DrawRect(info.Rect, paint);
            }
        }
    }
}