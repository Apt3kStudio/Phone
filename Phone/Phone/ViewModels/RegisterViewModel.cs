using Android.Content;
using Android.Widget;
using Phone.Services;
using Phone.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phone.ViewModels
{
   public class RegisterViewModel
    {
     private WebPortalApiServices _portalApiService = new WebPortalApiServices();
        private Context _context;
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public string Message { get; set; }


        INavigation Navigation;

        public RegisterViewModel(INavigation HomePageNav, Context context)
        {
            _context = context;
            Navigation = HomePageNav;
        }
        public ICommand  RegisterCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var message = await _portalApiService.RegisterAsync(Email, Password, ConfirmPassword);
                    if (message.isSuccess)
                    {
                        message.MessageText = "Registration Successfully!";
                        Toast.MakeText(_context, message.MessageText, ToastLength.Long).Show();
                        await Navigation.PushModalAsync(new NavigationPage(new Login(Email,_context)));

                    }
                    else
                    {                       
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
