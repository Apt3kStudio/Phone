using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Phone.Services;
using Phone.Views;
using Phone.ViewModels;
using System.IO;
using Xamarin.Essentials;
#region Firebase disabled
//using Plugin.FirebasePushNotification; 
#endregion
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
        
        private Context _context;
        public App(Context context)
        {
            @Phone.App StartTheApplication;
            _context = Android.App.Application.Context;
            InitializeComponent();
            //FCMService.InitializeComponents();    
           MainPage = new Account(_context).IsUseregistered(true);
           
        }        
    }
}
