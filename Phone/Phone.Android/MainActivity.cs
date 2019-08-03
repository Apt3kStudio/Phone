using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
#region MyRegion Firebase disabled
//using Plugin.FirebasePushNotification; 
#endregion
using Firebase.Messaging;
using System.Threading.Tasks;
using Firebase.Iid;
using Phone.Services;
using System.Threading;
using Phone.Models;
using Phone.Droid.Models;
using Xamarin.Forms;
using Calligraphy;
using System.Net;

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
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CalligraphyConfig.InitDefault(new CalligraphyConfig.Builder()
                  .SetDefaultFontPath("fonts/Righteous-Regular.ttf")
                  .SetFontAttrId(Resource.Attribute.fontPath)
              // Adding a custom view that support adding a typeFace
              // .AddCustomViewWithSetTypeface(Java.Lang.Class.FromType(typeof(CustomViewWithTypefaceSupport)))
              // Adding a custom style
              // .AddCustomStyle(Java.Lang.Class.FromType(typeof(TextField)), Resource.Attribute.textFieldStyle)
              .Build()
          );
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FormsMaterial.Init(this, savedInstanceState);


            LoadApplication(new App(this));


            #region Registering Xamarin Essentials on android;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            #endregion

            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
            #region Firebase disabled
            //FirebasePushNotificationManager.ProcessIntent(this, Intent); 
            #endregion
            #region call firebase            

            FirebaseMessaging.Instance.SubscribeToTopic("admin");
            var FirebaseID = Firebase.Iid.FirebaseInstanceId.Instance.Token;

            #endregion
            #region watch => phone communication
            new Task(() =>
            {
                cmm = new Communicator(this);
                cmm.SendMessage("FromPhone" + DateTime.Now.ToString("T"));
                cmm.DataReceived += DeviceReceived;

            });

            //cmm.DataReceived

            #endregion
            StartService(new Intent(this, Class));

        }
        private void DeviceReceived(Android.Gms.Wearable.DataMap device)
        {

            var TimeStamp = device.GetString("TimeStamp");
        }

        protected override void OnResume()
        {
            base.OnResume();

            //   cmm.Resume();
        }

        protected override void OnPause()
        {
            // cmm.Pause();

            base.OnPause();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void AttachBaseContext(Context context)
        {
            base.AttachBaseContext(CalligraphyContextWrapper.Wrap(context));
        }
    }
}