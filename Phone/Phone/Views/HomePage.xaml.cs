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
using Android.Content;
using Phone.Services;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BottomBarPage
    {
        private Context _context;
        private EventViewModel eventModel;
        public string strtimestamp { get; set; }
        public HomePage(Context context)
        {
            _context = context;
            eventModel = new EventViewModel(context);
            InitializeComponent();
            strtimestamp = "00000230000000000093";
            BindingContext = this;

        }
        public void OnTapGestSoundIcon(object sender, EventArgs args)
        {
            Task.Run(async () =>
            {
                await eventModel.PlaySound(10);
                await eventModel.setOption("option1");
            });
        }
        public void OnTapGestVibrate(object sender, EventArgs args)
        {           
            eventModel.VibrateMe(5);
            eventModel.VibrateWatch(5);
            Task.Run(async () =>
            {                
                await eventModel.setOption("option2");
            });
        }
        public void OnTapGestFlash(object sender, EventArgs args)
        {
            Task.Run(async () => 
            {
                await eventModel.FlashPattern();
                await eventModel.setOption("option3");
            });
        }
        async void LogOut(object sender, EventArgs e)
        {

            await SecureStorage.SetAsync("Email", ""); // remove the login flag from the application settings. 
            Application.Current.MainPage = new Login(_context);

            // await Navigation.PushModalAsync(new NavigationPage(new Login()));
        }
        async void saveOption1(object sender, EventArgs e)
        {
           
            await eventModel.setOption("option1");
            //var option = eventModel.getOption().Result;
            await eventModel.TriggerFeatureAsync();
        }
        async void saveOption2(object sender, EventArgs e)
        {
           
            await eventModel.setOption("option2");
           // var option = eventModel.getOption().Result;
            await eventModel.TriggerFeatureAsync();
        }
        async void saveOption3(object sender, EventArgs e)
        {
           
            await eventModel.setOption("option3");
            //var option = eventModel.getOption().Result;
            await eventModel.TriggerFeatureAsync();
        }

        public void PushVibrate_Clicked(object sender, EventArgs e)
        {           
            eventModel.VibrateMe(20);            
        }
        public void PushVibrateWatch_Clicked(object sender, EventArgs e)
        {           
            eventModel.VibrateWatch(20);
        }
        async void Flash_On(object sender, EventArgs e)
        {
           
            await eventModel.FlashLighOnAsync();
        }

        async void Flash_Off(object sender, EventArgs e)
        {
           
            await eventModel.FlashLighOffAsync();
        }
        async void Play_Sound(object sender, EventArgs e)
        {
           
            await eventModel.PlaySound(10);
        }
        async void Send_Email(object sender, EventArgs e)
        {
           
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
           
            string MessageText = "Test Text message send directly from 2nd Eye app";
            List<string> recipients = new List<string>();
            recipients.Add("3473314385@tmomail.net");
            recipients.Add("6463715196@tmomail.net");
            recipients.Add("3472009415@tmomail.net");

            await eventModel.SendEmailViaSMTP("tesT", MessageText, recipients);
        }
        async Task ResetFCMTokenAsync(object sender, EventArgs e)
        {
          //  await FCMService.DeleteFCMTokenAsync();

            WebPortalApiServices wapi = new WebPortalApiServices();
            await wapi.SendFCMTokenAsync();
        }
    }
}