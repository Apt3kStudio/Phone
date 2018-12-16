using Plugin.LocalNotifications;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartUpPage : TabbedPage
    {
       
        public StartUpPage()
        {
            InitializeComponent();
            DisplayAlert("This is an alert", "This is the alert Message", "This alert has been cancelled");
            var platf = DeviceInfo.Platform;
            CrossLocalNotifications.Current.Show("Device Platform", platf.ToString());

        }
    }
}