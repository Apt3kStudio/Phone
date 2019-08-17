
using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using Phone.Services;
using Phone.Models;
using Android.Widget;
using Android.Content;

namespace Phone.Droid.Models
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        private Context _context;
        const string TAG = "MyFirebaseIIDService";
        public MyFirebaseIIDService(Context context)
        {
            _context = context;
        }
        public MyFirebaseIIDService()
        { }
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            FCMService.CheckStoredToken(refreshedToken);
            MessageReceived(refreshedToken);
        }
        public void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed. 
        }
        // Events for incoming message or update data
        public event Action<string> MessageReceived = delegate
        {

        };
    }
}