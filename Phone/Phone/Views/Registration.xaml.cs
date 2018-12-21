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
    public partial class Registration : ContentPage
    {
        RegisterViewModel RegisterVM { get; set; }

        public Registration()
        {
            InitializeComponent();
            RegisterVM = new RegisterViewModel()
            {
                Email = "Admin@Admin.com",
                Password = "Password@132",
                ConfirmPassword = "Password@132"
            };
        
           // var s = RegisterVM.RegisterCommand.Execute(null);
            BindingContext = RegisterVM;
        }

        async void BackToLogin(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Login()));
        }
    }
    
}