using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Phone.Services;
using Phone.Views;
using Phone.ViewModels;
using System.IO;
using Xamarin.Essentials;
using Plugin.FirebasePushNotification;
using Plugin.LocalNotifications;
using Android.Content;
using Android.Util;
using Firebase.Iid;
using System.Threading.Tasks;
using Phone.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Phone
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;       
        private Context _context;
        
        public App(Context context)
        {
            _context = context;
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();           
            Task.Run(async() =>
            {
                await FCMService.SaveFCMTokenAsync(FCMService.getFCMToken());
                await FCMService.RegisterOnTokenRefreshAsync();
            });            
            FCMService.RegisterOnNotificationReceived();
            FCMService.RegisterOnNotificationOpened();   
            MainPage = new Account(_context).IsUseregistered(false);
         } 
    }
}
