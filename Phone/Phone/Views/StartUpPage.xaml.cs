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
           
            var platf = DeviceInfo.Platform;
            CrossLocalNotifications.Current.Show("Device Platform", platf.ToString());

        }
    }
}