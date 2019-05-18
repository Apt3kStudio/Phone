using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Wearable.Activity;
using Java.Interop;
using Android.Views.Animations;
using Android.Support.Wearable.View.Drawer;
using Xamarin.Essentials;
using Android.Gms.Wearable;
using Android.Gms.Common;

namespace SpideySenseWatch
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity, View.IOnCreateContextMenuListener
    {
        public int FORGOT_PHONE_NOTIFICATION_ID = 1;
        Communicator objCommunicator;
        #region navigation
        private Section DEFAULT_SECTION = Section.Vibrate;
        private WearableNavigationDrawer mWearableNavigationDrawer;
        private WearableActionDrawer mWearableActionDrawer;
        #endregion
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main2);

            #region NVD
            mWearableNavigationDrawer = (WearableNavigationDrawer)FindViewById(Resource.Id.top_navigation_drawer);
            //mWearableNavigationDrawer.SetAdapter(new NavigationAdapter(this, mWearableNavigationDrawer, mWearableActionDrawer));

            #endregion
            SectionFragment f = new SectionFragment();
            objCommunicator = new Communicator(this);
            objCommunicator.MessageReceived += delegate
            {
                string isVibrate = "";
                try
                {

                    Vibration.Vibrate();
                    var duration = TimeSpan.FromSeconds(20);
                    Vibration.Vibrate(duration);
                    isVibrate = "Success";
                }
                catch (FeatureNotSupportedException ex)
                {
                    isVibrate = "Not Supported";
                }
                catch (System.Exception ex)
                {
                    isVibrate = "Error";
                }
                // var v = CrossVibrate.Current;
                //v.Vibration(TimeSpan.FromSeconds(1)); // 1 second vibration
            };

            SectionFragment sectionFragment = f.GetSection(DEFAULT_SECTION);
            var sFFB = this.FragmentManager.BeginTransaction();
            sectionFragment.OnClick(f.View);
            sFFB.Replace(Resource.Id.fragment_container, sectionFragment);
            sFFB.Commit();

            mWearableActionDrawer = (WearableActionDrawer)FindViewById(Resource.Id.bottom_action_drawer);
            mWearableNavigationDrawer.SetAdapter(new NavigationAdapter(this, this.FragmentManager, mWearableNavigationDrawer, mWearableActionDrawer, objCommunicator));
            mWearableActionDrawer.MenuItemClick += (m, arg) =>
            {
                mWearableActionDrawer.CloseDrawer();
                switch (arg.Item.ItemId)
                {
                    case Resource.Id.action_edit:
                        Toast.MakeText(this, Resource.String.action_edit_todo, ToastLength.Short).Show();
                        //return true;
                        break;
                    case Resource.Id.action_share:
                        Toast.MakeText(this, Resource.String.action_share_todo, ToastLength.Short).Show();
                        // return true;
                        break;
                }

                // return false;
            };
            SetAmbientEnabled();
        }
        public bool OnMenuItemClick(IMenuItem menuItem)
        {
            mWearableActionDrawer.CloseDrawer();
            switch (menuItem.ItemId)
            {
                case Resource.Id.action_edit:
                    Toast.MakeText(this, Resource.String.action_edit_todo, ToastLength.Short).Show();
                    return true;
                case Resource.Id.action_share:
                    Toast.MakeText(this, Resource.String.action_share_todo, ToastLength.Short).Show();
                    return true;
            }
            return false;
        }
        #region DO NOT remove
        protected override void OnResume()
        {
            base.OnResume();

            objCommunicator.Resume(); // register listeners 
        }

        protected override void OnPause()
        {
            objCommunicator.Pause();

            base.OnPause();
        }
        #endregion
        private void Cm_DataReceived(DataMap obj)
        {

        }
        public void OnConnected(Bundle connectionHint)
        {

        }
        public void OnConnectionSuspended(int cause)
        {

        }

        public void OnConnectionFailed(ConnectionResult result)
        {

        }

    }
}


