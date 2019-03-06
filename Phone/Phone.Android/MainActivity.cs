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

namespace Phone.Droid
{
    [Activity(Label = "Phone", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Context AppContext { get; private set; }
        Communicator cmm;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
         
            #region Registering Xamarin Essentials on android;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            #endregion
            
            LoadApplication(new App());

            #region call firebase
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            FirebaseMessaging.Instance.SubscribeToTopic("admin");
            #endregion
            #region watch => phone communication
            cmm = new Communicator(this);
          
            cmm.SendMessage("FromPhone"+ DateTime.Now.ToString("T"));
            cmm.DataReceived += Cmm_DataReceived;
            //cmm.DataReceived


            #endregion

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