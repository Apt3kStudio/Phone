using Android.Content;
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
        private Context _context;
        public Registration(Context context)
        {
            _context = context;
            InitializeComponent();
            RegisterVM = new RegisterViewModel(Navigation, _context)
            {
                Email = "",
                Password = "",
                ConfirmPassword = ""
            };
        
           // var s = RegisterVM.RegisterCommand.Execute(null);
            BindingContext = RegisterVM;
        }

        async void BackToLogin(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Login(_context)));
        }
    }
    
}