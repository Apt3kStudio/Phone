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

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Phone
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;
        static DeviceLocalDbService db;
        private Context _context;
        
        public App(Context context)
        {
            _context = context;
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            db = Database;

            MainPage = IsUseregistered();
            setFierbaseAppResources();

        }

        private static void setFierbaseAppResources()
        {
            string FirebaseID = getAndStoreFBToken();

            System.Diagnostics.Debug.WriteLine($"TOKEN:{FirebaseID}");
            //Log.Info("TOKEN:", FirebaseID.ToString());

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN:{p.Token}");
                Log.Info("TOKEN:", p.Token.ToString());
               
            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                //   CrossLocalNotifications.Current.Show(p.Data["title"].ToString(), p.Data["body"].ToString());
                // System.Diagnostics.Debug.WriteLine("Received");
            };
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");

                foreach (var data in p.Data)
                {
                    EventViewModel evm = new EventViewModel();

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _ = evm.setOption("option1");
                        await evm.TriggerFeatureAsync();
                    });
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }
            };
        }

        private static string getAndStoreFBToken()
        {
            var FirebaseID = FirebaseInstanceId.Instance.Token;
            Task.Run(async () =>
            {
                await SecureStorage.SetAsync("FBToken", FirebaseID.ToString());
            });
            return FirebaseID;
        }

        private Page IsUseregistered()
        {
            if (LoginUserViewModel.IsUseregisteredAsync().Result)
            {
                return new NavigationPage(new HomePage(_context));
            }
            else
            {
               return new Login(_context);
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
