using Android.Content;
using Android.Widget;
using Phone.Models;
using Phone.Services;
using Phone.Views;
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

        private WebPortalApiServices _portalApiService = new WebPortalApiServices();
        public string Email { get; set; }
        public string Password { get; set; }

        public string Message { get; set; }

        public bool isAuthenticated { get; set; }

        INavigation Navigation;
        public LoginUserViewModel(INavigation HomePageNav, Context context)
        {
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

    }
}
