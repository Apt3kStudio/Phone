using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Support.Wearable.View.Drawer;
using SpideySenseWatch.Services;
using SpideySenseWatch.Models;

namespace SpideySenseWatch
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity, View.IOnCreateContextMenuListener
    {
        public int FORGOT_PHONE_NOTIFICATION_ID = 1;
        ConnectionService connService;
        #region navigation
        private Section DEFAULT_SECTION = Section.Vibrate;
        private WearableNavigationDrawer mWearableNavigationDrawer;
        private WearableActionDrawer mWearableActionDrawer;
        #endregion
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            connService = new ConnectionService();
            SetContentView(Resource.Layout.activity_main2);            
            #region NVD
            mWearableNavigationDrawer = (WearableNavigationDrawer)FindViewById(Resource.Id.top_navigation_drawer);            
            #endregion
            SectionFragment f = new SectionFragment();
            
            SectionFragment sectionFragment = f.GetSection(DEFAULT_SECTION);
            var sFFB = this.FragmentManager.BeginTransaction();
            sectionFragment.OnClick(f.View);
            sFFB.Replace(Resource.Id.fragment_container, sectionFragment);
            sFFB.Commit();

            mWearableActionDrawer = (WearableActionDrawer)FindViewById(Resource.Id.bottom_action_drawer);
            mWearableNavigationDrawer.SetAdapter(new NavigationAdapter(this, this.FragmentManager, mWearableNavigationDrawer, mWearableActionDrawer));
            mWearableActionDrawer.MenuItemClick += (m, arg) =>
            {
                mWearableActionDrawer.CloseDrawer();                
            };
            SetAmbientEnabled();
        }
        public bool OnMenuItemClick(IMenuItem menuItem)
        {            
            return false;
        }
        protected override void OnResume()
        {
            base.OnResume();
            connService.Resume(); // register listeners 
        }
        protected override void OnPause()
        {
            connService.Pause();
            base.OnPause();
            

          
        }                   
    }
}


