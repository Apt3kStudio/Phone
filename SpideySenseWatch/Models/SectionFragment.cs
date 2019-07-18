using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace SpideySenseWatch.Models
{
    public class SectionFragment : Fragment, View.IOnClickListener, View.IOnContextClickListener
    {

        public SectionFragment GetSection(Section section)
        { 
            
            SectionFragment newSection = new SectionFragment();
            Bundle arguments = new Bundle();
         //   var serializedobje = ObjectToByteArray(section);
            var cvc = (ISerializable)section;
            arguments.PutSerializable(EXTRA_SECTION, cvc);
            newSection.Arguments = arguments;
        
            return newSection;
        }
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        public static readonly System.String EXTRA_SECTION = "com.companyname.Phone.EXTRA_SECTION";

        private Section mSection;
        private ImageView mEmojiView;
        private TextView mTitleView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            
            View view = inflater.Inflate(Resource.Layout.fragment_section, container, false);
           

            mEmojiView = (ImageView)view.FindViewById(Resource.Id.emoji);
            mTitleView = (TextView)view.FindViewById(Resource.Id.title);
            #region we might need this, idk yet
            if (Arguments != null)
            {
                mSection = (Section)Arguments.GetSerializable(EXTRA_SECTION);
                Drawable imageDrawable = ContextCompat.GetDrawable(Context, mSection.drawableRes);
                mEmojiView.SetImageDrawable(imageDrawable);
                mTitleView.SetText(mSection.titleRes);
            }
            #endregion
          
            return view;
        }

        public void OnClick(View v)
        {
          //  Toast.MakeText(this, Resource.String.action_share_todo, ToastLength.Short).Show();
        }

        public bool OnContextClick(View v)
        {
            return true;
        }
    }
    #region dr
    public class Section:Java.Lang.Object, ISerializable
    {
        public int titleRes;
        public int drawableRes;
        public static Section Vibrate = new Section(Resource.String.sun_title, Resource.Drawable.vibrate);
        public static Section Flash = new Section(Resource.String.moon_title, Resource.Drawable.flash);
        public static Section Sound = new Section(Resource.String.earth_title, Resource.Drawable.sound);
        public static Section Settings = new Section(Resource.String.settings_title, Resource.Drawable.ic_settings_black_24dp);
        public static IEnumerable<Section> Values
        {
            get
            {
                yield return Vibrate;
                yield return Flash;
                yield return Sound;
                yield return Settings;

            }
        }

       // public IntPtr Handle => throw new NotImplementedException();

        public Section(int titleRes, int drawableRes)
        {
            this.titleRes = titleRes;
            this.drawableRes = drawableRes;
        }

        //public void Dispose()
        //{
           
        //}
    }
    #endregion
}