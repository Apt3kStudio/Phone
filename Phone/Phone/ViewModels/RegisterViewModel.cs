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

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public string Message { get; set; }


        INavigation Navigation;

        public RegisterViewModel(INavigation HomePageNav)
        {
            Navigation = HomePageNav;
        }
        public ICommand  RegisterCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var isSuccess = await _portalApiService.RegisterAsync(Email, Password, ConfirmPassword);
                    if (isSuccess)
                    {
                        Message = "Registration Successfully!";
                        await Navigation.PushModalAsync(new NavigationPage(new Login(Email)));
                    }
                    else
                    {
                        Message = "Try again.";
                    }
                });
            }
            set
            {

            }
        }

    }
}
