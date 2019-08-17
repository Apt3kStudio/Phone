using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Phone.ViewModels;
#region Firebase disabled
//using Plugin.FirebasePushNotification; 
#endregion


namespace Phone.Droid.Models
{
   
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                #region Firebase disabled
                // FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel"; 
                #endregion

                //Change for your default notification channel name here
                #region Firebase disabled
                //FirebasePushNotificationManager.DefaultNotificationChannelName = "General"; 
                #endregion
            }


            //If debug you should reset the token each time.
#if DEBUG
            #region Firebase disabled
            //FirebasePushNotificationManager.Initialize(this, true); 
            #endregion
#else
              FirebasePushNotificationManager.Initialize(this,false);
#endif

            //Handle notification when app is closed here
            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{

            //    EventViewModel evm = new EventViewModel();

            //    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {
            //      await evm.setOption("option1");
            //        await evm.TriggerFeatureAsync();
            //    });
            //};
            //var FirebaseID = Firebase.Iid.FirebaseInstanceId.Instance.Token;


        }
    }
}