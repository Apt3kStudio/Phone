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
        public Login()
        {
            InitializeComponent();
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
            // await DisplayAlert("SignUp", "You will be redirected to the Registration form", "OK");
            await Navigation.PushModalAsync(new NavigationPage(new StartUpPage()));
        }
    }
}