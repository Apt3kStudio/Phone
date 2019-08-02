using Android.Content;
using Android.Widget;
using Phone.Models;
using Phone.Services;
using Phone.Views;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Phone.ViewModels
{
    public class AuthVM: BaseVM
    {
        public AuthVM(INavigation HomePageNav, Context context)
        {
            canvasView = new SKCanvasView();
            
            _portalApiService = new WebPortalApiServices();
            _context = context;
            Navigation = HomePageNav;
        }

        public AuthVM()
        {
            canvasView = new SKCanvasView();
            _portalApiService = new WebPortalApiServices();
            SigningInCommand = new Command(SigningIn);
            RegisterCommand = new Command(Register);
            LogOutCommand = new Command(async()=> await LogOut());
        }

        public string Email { get; set; }
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }
        public bool isAuthenticated { get; set; }
        bool _isBussy = false;
        public SKCanvasView canvasView;
        private Context _context;
        private WebPortalApiServices _portalApiService;

        public ICommand SigningInCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand LogOutCommand { get;}

        private string _ErrorMessage { get; set; }
        public string ErrorMessage
        {
            get => _ErrorMessage;
            set
            {
                if (_ErrorMessage == value)
                    return;
                _ErrorMessage = value;
                NotifyPropertyChange(nameof(ErrorMessage));
            }
        }
       
        INavigation Navigation;
      
        public static async System.Threading.Tasks.Task<bool> IsUseregisteredAsync()
        {
            var Email = await SecureStorage.GetAsync("Email");
            
            return (!string.IsNullOrEmpty(Email))?true:false; 
        }
        public void SigningIn()
        {
            Task.Run(async () => {
                isBussy = true;
                var message = await _portalApiService.SigningIn(Email, Password);
                if (message.isSuccess)
                {
                    isAuthenticated = true;
                    await SecureStorage.SetAsync("Email", Email);
                    try
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(async() =>
                        {
                            try
                            {
                                Application.Current.MainPage = new AppShell();
                                //await Shell.Current.Navigation.PushAsync(new DevicesHome());
                            }
                            catch (Exception e)
                            {
                                

                            }
                            
                        });
                    }
                    catch (Exception e)
                    {

                       // throw;
                    }

                }
                else
                {
                    isAuthenticated = false;
                    Console.WriteLine("Error Login in");
                    System.Diagnostics.Debug.WriteLine("Error", "login");
                }
                isBussy = false;
            });
        }
        public void Register()
        {
            Task.Run(async() =>
                {
                    isBussy = true;
                    var message = await _portalApiService.RegisterAsync(Email, Password, ConfirmPassword);
                    if (message.isSuccess)
                    {
                        Toast.MakeText(_context, message.MessageText, ToastLength.Long).Show();

                        Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Navigation.PushModalAsync(new NavigationPage(new Login(Email)));
                        });
                        
                    }
                    else
                    {
                       
                        Console.WriteLine("Error Login in");
                    }
                    isBussy = false;
                });
            
        }
        async Task LogOut()
        {

            await SecureStorage.SetAsync("Email", ""); // remove the login flag from the application settings. 
            Application.Current.MainPage = new Login();

            // await Navigation.PushModalAsync(new NavigationPage(new Login()));
        }


    }
}
