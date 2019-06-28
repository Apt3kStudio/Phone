using Android.Util;
using Firebase.Iid;
using Phone.ViewModels;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Phone.Services
{
    class FCMService
    {           
        public static string getFCMToken()
        {
            Console.WriteLine($"FCM-Token {FirebaseInstanceId.Instance.Token}");
            return FirebaseInstanceId.Instance.Token;  
        }       
        public static async Task RegisterOnTokenRefreshAsync()
        {
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN:{p.Token}");
                Console.WriteLine($"TOKEN-Refreshed:{p.Token.ToString()}");
            };
           await SaveFCMTokenAsync(getFCMToken());
            Console.WriteLine($"FCM-RegisterOnTokenRefresh()");
        }
        internal static void InitializeComponents()
        {
                Task.Run(async () =>
                {
                    await SaveFCMTokenAsync(getFCMToken());
                    await RegisterOnTokenRefreshAsync();
                });
                RegisterOnNotificationReceived();
                RegisterOnNotificationOpened();        
        }
        public static void RegisterOnNotificationReceived()
        {
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

            };
            Console.WriteLine($"FCM-RegisterOnNotificationReceived()");
        }
        public static void RegisterOnNotificationOpened()
        {
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
            Console.WriteLine($"FCM-RegisterOnNotificationOpened()");
        }
        public static async Task SaveFCMTokenAsync(string Token)
        {           
            await SecureStorage.SetAsync("FBToken", Token.ToString());           
            Console.WriteLine($"FCM-SaveFCMToken() { Token.ToString()}");
        }
        public static async Task DeleteFCMTokenAsync()
        {
            Console.WriteLine($"FCM-Old Token: {FirebaseInstanceId.Instance.Token}");
            await Task.Run(() =>
            {
                // This may not be executed on the main thread.               
                FirebaseInstanceId.Instance.DeleteInstanceId();
                
            });
           await SaveFCMTokenAsync(getFCMToken());
            Console.WriteLine("FCM-Forced New Token: " + FirebaseInstanceId.Instance.Token);
        }
    }
}
