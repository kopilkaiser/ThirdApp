
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View.Accessibiity;
using AndroidX.Fragment.App;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThirdApp
{
    public class PlayQuoteFragment : Fragment
    {

        #region 

        string something => Arguments.GetString("something", string.Empty);

        bool isSomething => Arguments.GetBoolean("isSomething", false);

        #endregion
        public int PlayId
        {
            get { return Arguments.GetInt("current_play_id", 0); }
        }
        
        public static PlayQuoteFragment NewInstance(Bundle bundle)
        {
            return new PlayQuoteFragment { Arguments = bundle };
        }
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
        }
        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
        }

  
        public override void OnStart()
        {
            base.OnStart();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            var textView = new TextView(Activity);
            var padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));
            textView.SetPadding(padding, padding, padding, padding);
            textView.TextSize = 24;
            textView.Text = TitlesFragment.Data.Description[PlayId];

            var scroller = new ScrollView(Activity);
            scroller.AddView(textView);

            return scroller;
        }

        public override void OnPause()
        {
            base.OnPause();
            Log.Debug("PlayQuoteFragment", "OnPause()");

        }
        public override void OnResume()
        {
            base.OnResume();
            Log.Debug("PlayQuoteFragment", "OnResume()");
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        public override void OnDetach()
        {
            base.OnDetach();
            Arguments.Dispose();
        }
    }
}