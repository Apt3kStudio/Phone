using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Wearable.View.Drawer;
using Android.Views;
using Android.Widget;
using SpideySenseWatch.Models;

namespace SpideySenseWatch.Models
{
    public class NavigationAdapter : WearableNavigationDrawer.WearableNavigationDrawerAdapter
    {
        
        private Context mContext;
        private Section mCurrentSection = Section.Vibrate;
        private readonly FragmentManager _fragmentManager;
        private Communicator _Communicator;
        public virtual FragmentManager GetFragmentManager()
        {
            return _fragmentManager;
        }

        private WearableNavigationDrawer mWearableNavigationDrawer;
        private WearableActionDrawer mWearableActionDrawer;

        public override int Count => 4;

        //  public NavigationAdapter(Context context, WearableNavigationDrawer mWearableNavigationDrawer1, WearableActionDrawer mWearableActionDrawer1)
        public NavigationAdapter(Context context, FragmentManager fragmentManager, WearableNavigationDrawer wnavDwe, WearableActionDrawer wActionDwr, Communicator objCommunicator )
        {
            mContext = context;
            _fragmentManager = fragmentManager;
            mWearableNavigationDrawer = wnavDwe;
            mWearableActionDrawer = wActionDwr;
            _Communicator = objCommunicator;
        }


        public override System.String GetItemText(int index)
        {
            Section selectedSection = getSection(index);
            return mContext.GetString(selectedSection.titleRes);
        }


        public override Drawable GetItemDrawable(int index)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Index: " +  index.ToString());
                Section selectedSection = getSection(index);
                return mContext.GetDrawable(selectedSection.drawableRes);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
                throw;
            }
            
        }

        public override void OnItemSelected(int index)
        {
            Section selectedSection = getSection(index);
            _Communicator.SetTriggerEvent(index);
            // Only replace the fragment if the section is changing.
            if (selectedSection == mCurrentSection)
            {
                return;
            }
            mCurrentSection = selectedSection;
            #region //url https://docs.microsoft.com/en-us/xamarin/android/platform/fragments/managing-fragments
            SectionFragment f = new SectionFragment();
            SectionFragment sectionFragment = f.GetSection(selectedSection);            
            GetFragmentManager().BeginTransaction()
            .Replace(Resource.Id.fragment_container, sectionFragment)
            .Commit();
            #endregion


            // No actions are available for the settings specific fragment, so the drawer
            // is locked closed. For all other SelectionFragments, it is unlocked.
            if (selectedSection == Section.Settings)
            {
                mWearableActionDrawer.LockDrawerClosed();
            }
            else
            {
                  mWearableActionDrawer.UnlockDrawer();
            }
        }

        private static Section getSection(int index)
        {
            int i = 0;
            Section selectedSection = null;
            foreach (var item in Section.Values)
            {
                System.Diagnostics.Debug.WriteLine("In Get Session Item: " + item.ToString());
                if (index == i)
                    selectedSection = item;
                i++;
            }
            System.Diagnostics.Debug.WriteLine("getSection return " +  selectedSection.ToString());
            return selectedSection;
        }

        public int GetCount()
        {
            var v = (List<Section>)Section.Values.GetEnumerator();
            return v.Count;
        }
    }
}