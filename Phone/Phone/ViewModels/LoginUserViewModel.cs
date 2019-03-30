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
       

        private WebPortalApiServices _portalApiService = new WebPortalApiServices();
        private DeviceLocalDbService _DeviceDb = new DeviceLocalDbService();
        public string Email { get; set; }
        public string Password { get; set; }

        public string Message { get; set; }

        public bool isAuthenticated { get; set; }

        INavigation Navigation;
        public LoginUserViewModel(INavigation HomePageNav)
        {
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
                    var isSuccess = await _portalApiService.SigningIn(Email, Password);
                    if (isSuccess)
                    {
                        isAuthenticated = true;
                        // await _DeviceDb.SaveItemAsync(new UserLoginSettings() { Email = Email, Password = Password, Token = "" });
                        await SecureStorage.SetAsync("Email", Email);
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
