using Phone.Models;
using Phone.Services;
using Phone.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phone.ViewModels
{
    public class LoginUserViewModel : IUser
    {
       

        private WebPortalApiServices _portalApiService = new WebPortalApiServices();
        public string Email { get; set; }
        public string Password { get; set; }

        public string Message { get; set; }

        public bool isAuthenticated { get; set; }
        INavigation Navigation;

        public LoginUserViewModel(INavigation HomePageNav)
        {
            Navigation = HomePageNav;
        }


        public Command SigningInCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isSuccess = await _portalApiService.SigningIn(Email, Password);
                    if (isSuccess)
                    {
                        isAuthenticated = true;

                        Message = "Registration Successfully!";
                        
                        await Navigation.PushModalAsync(new NavigationPage(new HomePage()));
                    }
                    else
                    {
                        isAuthenticated = false;
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
