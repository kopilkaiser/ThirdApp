
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;
using Android.App;


namespace ThirdApp
{
    [Activity(Label = "PlayQuoteActivity")]
    public class PlayQuoteActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape) {

                Finish();
                
            }

            var playId = Intent.Extras.GetInt("current_play_id", 0);
            var something = Intent.GetStringExtra("something");
            var isSomething = Intent.GetBooleanExtra("isSomething", false);

            Bundle bundle = new Bundle();
            bundle.PutInt("current_play_id", playId);
            bundle.PutString("something", something);
            bundle.PutBoolean("isSomething", isSomething);
        
            var playQuoteFragment = PlayQuoteFragment.NewInstance(bundle);

            SupportFragmentManager.BeginTransaction()
                .Add(Android.Resource.Id.Content, playQuoteFragment)
                .Commit();
            
            // Create your application here
        }
    }
}