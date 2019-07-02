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
        public static string GetFCMToken()
        {
            Log.Info("FCM-Token", FirebaseInstanceId.Instance.Token); 
            return FirebaseInstanceId.Instance.Token;  
        }
        public static string GetSavedFCMToken()
        {
            var savedToken = SecureStorage.GetAsync("FBToken").Result;
            if (savedToken == null)
            {
                savedToken = "";
            }
            Log.Info("FCM-GetSavedToken", savedToken);
            return savedToken;
        }
        public static async Task RegisterOnTokenRefreshAsync()
        {
            Log.Info("FCM-Token", "FCM-RegisterOnTokenRefresh()");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN:{p.Token}");
                Console.WriteLine($"*******************TOKEN-Refreshed:{p.Token.ToString()}");
                Log.Info("***********************FCM-Token-Refreshed", p.Token.ToString());
                CheckStoredToken(p.Token);
            };
            await SaveFCMTokenAsync(GetFCMToken());
                    
        }
        internal static void InitializeComponents()
        {               
            RegisterOnNotificationReceived();
            RegisterOnNotificationOpened();              
            Log.Info("FCM", "InitializeComponents");
        }
        public static void RegisterOnNotificationReceived()
        {
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
               
                foreach (var data in p.Data)
                {
                    if (data.Key == "Option")
                    {
                        EventViewModel evm = new EventViewModel();
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _ = evm.setOption(data.Value.ToString());
                            await evm.TriggerFeatureAsync();
                        });                       
                    }
                    Log.Info($"{data.Key}", $"{data.Value}");
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }                
                Log.Info("FCM-Fired", "CrossFirebasePushNotification.Current.OnNotificationReceived");
            };
            Log.Info("FCM", "RegisterOnNotificationReceived");            
        }
        public static void RegisterOnNotificationOpened()
        {
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
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
                Log.Info("FCM-fired", "CrossFirebasePushNotification.Current.OnNotificationOpened");
            };
            Log.Info("FCM", "RegisterOnNotificationOpened");
        }
        public static async Task SaveFCMTokenAsync(string Token)
        {           
            await SecureStorage.SetAsync("FBToken", Token.ToString());                       
            Log.Info("FCM", "SaveFCMTokenAsync");
        }
        public static async Task DeleteFCMTokenAsync()
        {
            Log.Info("FCM", "DeleteFCMTokenAsync");
            Log.Info("FCM-Old-Token:-:-:", FirebaseInstanceId.Instance.Token);            
            await Task.Run(() =>
            {
                Log.Info("FCM-Delete-Started", "");
                FirebaseInstanceId.Instance.DeleteInstanceId();
                Log.Info("FCM-Delete-Finished", "");
                Log.Info("FCM-New-Token:-:-:", FirebaseInstanceId.Instance.Token);
            });
            await SaveFCMTokenAsync(GetFCMToken());
            Log.Info("FCM-New-Token:-:-:", FirebaseInstanceId.Instance.Token);          
        }
        public static void CheckStoredToken(string token)
        {
            if (GetSavedFCMToken() != token)
            { 
                Task.Run(async () => {
                    await SaveFCMTokenAsync(token);
                    WebPortalApiServices wps = new WebPortalApiServices();
                    await wps.SendFCMTokenAsync();
                    Log.Info("FCM-New-Token-Sent-To-Portal:-:-:", token);
                });

            }           
        }
    }
}
