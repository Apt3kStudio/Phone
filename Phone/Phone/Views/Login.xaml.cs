using Phone.Services;
using Phone.ViewModels;
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
    public partial class Login : ContentPage
    {
        private WebPortalApiServices apiService = new WebPortalApiServices();
       public LoginUserViewModel UserVM { set; get; }
        public string ErrorMessage { get; set; }
        public Login()
        {
            InitializeComponent();


            UserVM = new LoginUserViewModel() {
                 Email = "",
                 Password = ""
            };


            BindingContext = UserVM;

            //var authenticator = new OAuth2Authenticator(
            //clientId,
            //null,
            //Constants.Scope,
            //new Uri(Constants.AuthorizeUrl),
            //new Uri(redirectUri),
            //new Uri(Constants.AccessTokenUrl),
            //null,
            //true);
        }
        async void NavigateToRegistrationPage_Clicked(object sender, EventArgs e)
        {
           // await DisplayAlert("SignUp", "You will be redirected to the Registration form", "OK");
            await Navigation.PushModalAsync(new NavigationPage(new Registration()));
        }
        async void goToStartUpPage(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(UserVM.Email)&& !string.IsNullOrEmpty(UserVM.Password))
            {
                UserVM.SigningInCommand.Execute(null);
                if (UserVM.isAuthenticated)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new StartUpPage()));
                }
                await DisplayAlert("Login", UserVM.Message, "OK");
            }

         

            // await DisplayAlert("SignUp", "You will be redirected to the Registration form", "OK");


        }
    }
}