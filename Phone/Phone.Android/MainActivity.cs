using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Plugin.FirebasePushNotification;
using Firebase.Messaging;
using System.Threading.Tasks;
using Firebase.Iid;

namespace Phone.Droid
{
    [Activity(Label = "SpideySense", Icon = "@drawable/Spide_Icon_WhiteBlue", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Context AppContext { get; private set; }
        Communicator cmm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
         
            #region Registering Xamarin Essentials on android;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            #endregion
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            LoadApplication(new App(this));

            #region call firebase            
            //Task.Run(() =>
            //{
            //    // This may not be executed on the main thread.
            //    FirebaseInstanceId.Instance.DeleteInstanceId();
            //    Console.WriteLine("Forced token: " + FirebaseInstanceId.Instance.Token);
            //});
            FirebaseMessaging.Instance.SubscribeToTopic("admin");
            var FirebaseID = Firebase.Iid.FirebaseInstanceId.Instance.Token;           

            #endregion
            #region watch => phone communication
            cmm = new Communicator(this);
          
            cmm.SendMessage("FromPhone"+ DateTime.Now.ToString("T"));
            cmm.DataReceived += Cmm_DataReceived;
            //cmm.DataReceived

            #endregion
            StartService(new Intent(this,Class));
        }

        private void Cmm_DataReceived(Android.Gms.Wearable.DataMap obj)
        {
            throw new NotImplementedException();
        }

        protected override void OnResume()
        {
            base.OnResume();

            cmm.Resume();
        }

        protected override void OnPause()
        {
            cmm.Pause();

            base.OnPause();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}