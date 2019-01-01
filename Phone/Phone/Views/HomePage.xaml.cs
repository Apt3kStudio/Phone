using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Phone.Utility;
using Phone.ViewModels;
using Plugin.LocalNotifications;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BottomBarPage
    {
        public HomePage()
        {
            InitializeComponent();
            CrossLocalNotifications.Current.Show("title", "body");
        }
        async void LogOut(object sender, EventArgs e)
        {

            await SecureStorage.SetAsync("Email", ""); // remove the login flag from the application settings. 
            Application.Current.MainPage = new Login();

            // await Navigation.PushModalAsync(new NavigationPage(new Login()));
        }
        void PushVibrate_Clicked(object sender, EventArgs e)
        {
            EventViewModel eventModel = new EventViewModel();
            eventModel.VibrateMe(20);
        }
        async void Flash_On(object sender, EventArgs e)
        {
            EventViewModel eventModel = new EventViewModel();
            await eventModel.FlashLighOnAsync();
        }

        async void Flash_Off(object sender, EventArgs e)
        {
            EventViewModel eventModel = new EventViewModel();
            await eventModel.FlashLighOffAsync();
        }


    }
}