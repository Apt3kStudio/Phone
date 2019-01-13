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
using System.IO;
using System.Resources;
using System.Reflection;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BottomBarPage
    {
        public HomePage()
        {
            InitializeComponent();
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
        async void Play_Sound(object sender, EventArgs e)
        {
            EventViewModel eventModel = new EventViewModel();
            await eventModel.PlaySound();
        }
        async void Send_Email(object sender, EventArgs e)
        {
            EventViewModel eventModel = new EventViewModel();
            string subject = "Test Email FROM PHONE";
            string body = "This email is to test the phone's ability to send out email notification";
            List<string> recipients = new List<string>();
            recipients.Add("nelsonvasquez03@gmail.com");
            recipients.Add("jtoonz16@gmail.com");
            recipients.Add("dionelrodriguez16@gmail.com");
            recipients.Add("Dioscarr@gmail.com");

            await eventModel.SendEmailViaSMTP(subject, body, recipients);
        }

        async void Send_SMS(object sender, EventArgs e)
        {
            EventViewModel eventModel = new EventViewModel();
            string MessageText = "Test Text message send directly from 2nd Eye app";
            List<string> recipients = new List<string>();
            recipients.Add("3473314385@tmomail.net");
            recipients.Add("6463715196@tmomail.net");
            recipients.Add("3472009415@tmomail.net");

            await eventModel.SendEmailViaSMTP("tesT", MessageText, recipients);
        }
    }
}