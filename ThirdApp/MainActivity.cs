using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Service.QuickSettings;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.View.Accessibiity;

namespace ThirdApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        FrameLayout quotePlayContainer;
        FrameLayout titlesFragContainer;
        TitlesFragment titlesFragment;

        bool showingTwoFragments;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            titlesFragContainer = FindViewById<FrameLayout>(Resource.Id.titles_container);
            quotePlayContainer = FindViewById<FrameLayout>(Resource.Id.quoteContainer);
            
            titlesFragment = SupportFragmentManager.FindFragmentByTag("titlesFrag") as TitlesFragment;


            showingTwoFragments = quotePlayContainer != null && quotePlayContainer.Visibility == ViewStates.Visible;
            
            
            if (titlesFragment != null) { var titlesIsAdded = titlesFragment.IsAdded; }
            
            Bundle bundle = new Bundle();
            bundle.PutBoolean("show_two_fragments", showingTwoFragments);

            if (titlesFragment == null)
            {
                titlesFragment = TitlesFragment.NewInstance(bundle);
                SupportFragmentManager.BeginTransaction().Add(Resource.Id.titles_container, titlesFragment, "titlesFrag").Commit();
            }
            else 
            {
                titlesFragment.Arguments = bundle;

                var testBundle = titlesFragment.Arguments;
                SupportFragmentManager.BeginTransaction()             
               .Replace(titlesFragContainer.Id, titlesFragment)
               .Commit();
            }
           
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}