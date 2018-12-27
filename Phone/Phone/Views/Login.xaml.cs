using Phone.Services;
using Phone.ViewModels;
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
        public Login()
        {
            InitializeComponent();
            UserVM = new LoginUserViewModel(Navigation) {
                 Email = "",
                 Password = ""
            };
            BindingContext = UserVM;
        }

        public Login(string username)
        {
            InitializeComponent();
            UserVM = new LoginUserViewModel(Navigation)
            {
                Email = username,
                Password = ""
            };
            BindingContext = UserVM;
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
                UserVM.Message = "too soon!";
               
                UserVM.SigningInCommand.Execute(null);

               // LongRunningMethod();

                //if (UserVM.isAuthenticated)
                //{
                //   await Navigation.PushModalAsync(new NavigationPage(new HomePage()));
                //}
                //else if(!UserVM.isAuthenticated)
                //{
                //    await DisplayAlert("Login", UserVM.Message, "OK");
                //}
                 
            }
        }
      



    }
}