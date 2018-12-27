using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Phone.Services;
using Phone.Views;
using Phone.ViewModels;
using System.IO;
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Phone
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;
        static DeviceLocalDbService db;

        
        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            //MainPage = new StartUpPage();
          
            db = Database;
          
            if (LoginUserViewModel.IsUseregisteredAsync().Result)
            {
                MainPage = new NavigationPage( new HomePage());
            }
            else
            {
                MainPage = new Login();
            }
         
        }
        public static DeviceLocalDbService Database
        {
            get {
                if (db == null)
                {
                    db = new DeviceLocalDbService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ApplicationLoginSetting.db3"));
                }
                return db;
            }            
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
