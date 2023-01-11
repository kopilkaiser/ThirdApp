

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ThirdApp
{
    public class TitlesFragment : Fragment
    {
        int selectedPlayId;
        View view;
        ListView listView;
        ArrayAdapter adapter;

        public bool showingTwoFragments
        {
            get
            {
                return Arguments.GetBoolean("show_two_fragments", false);
            }

            private set { }
        }
        public class Data
        {
            public static string[] Titles =
            {
                "1", "2", "3"
            };

            public static string[] Description =
{
                "One", "Two", "Three"
            };
        }
        public static TitlesFragment NewInstance(Bundle bundle)
        {
            return new TitlesFragment { Arguments = bundle };
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null)
            {
                selectedPlayId = savedInstanceState.GetInt("current_play_id", 0);
                showingTwoFragments = savedInstanceState.GetBoolean("show_two_Fragments", false);
            }
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            view = inflater.Inflate(Resource.Layout.fragment_titles, container, false);

            listView = view.FindViewById<ListView>(Resource.Id.titles_list);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, Data.Titles);

            listView.Adapter = adapter;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            listView.ItemClick += ListView_ItemClick;

            /*var quoteContainer = Activity.FindViewById<FrameLayout>(Resource.Id.quoteContainer);

            showingTwoFragments = quoteContainer != null && quoteContainer.Visibility == ViewStates.Visible;*/

            if (showingTwoFragments)
            {
                listView.ChoiceMode = ChoiceMode.Single;
                ShowPlayQuote(selectedPlayId);
            }
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ShowPlayQuote(e.Position);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            //outState.PutBoolean("show_two_fragments", showingTwoFragments);
        }
        void ShowPlayQuote(int playId)
        {
            selectedPlayId = playId;

            if (showingTwoFragments)
            {
                var playQuoteFrag = ParentFragmentManager.FindFragmentByTag("quoteFrag") as PlayQuoteFragment;



                if (playQuoteFrag == null || playQuoteFrag.PlayId != playId)
                {
                    var quoteContainer = Activity.FindViewById<FrameLayout>(Resource.Id.quoteContainer);

                    Arguments.PutInt("current_play_id", playId);
                    Arguments.PutString("something", "yakuza");
                    Arguments.PutBoolean("isSomething", false);

                    var playQuoteFragTag = ParentFragmentManager.FindFragmentByTag("quoteFrag") as PlayQuoteFragment;

                    if (playQuoteFragTag == null)
                    {
                        var quoteFrag = PlayQuoteFragment.NewInstance(Arguments);
                        ParentFragmentManager.BeginTransaction()
                            .Replace(Resource.Id.quoteContainer, quoteFrag, "quoteFrag")
                            .AddToBackStack(null)
                            .SetTransition(FragmentTransaction.TransitFragmentFade)
                            .Commit();
                    }
                    else
                    {
                       // playQuoteFragTag = PlayQuoteFragment.NewInstance(Arguments);
                        Log.Debug("Something", "123");

                        ParentFragmentManager.BeginTransaction()
                        .Replace(Resource.Id.quoteContainer, playQuoteFragTag, "quoteFrag")
                        .AddToBackStack(null)
                        .SetTransition(FragmentTransaction.TransitFragmentFade)
                        .Commit();
                    }
                }
            }
            else
            {
                Intent intent = new Intent(Activity, typeof(PlayQuoteActivity));
                intent.PutExtra("current_play_id", playId);
                intent.PutExtra("something", "something");
                intent.PutExtra("isSomething", true);
                StartActivity(intent);
            }

        }

        public override void OnResume()
        {
            base.OnResume();
            Log.Debug("TitlesFragment", "OnResume");
        }
        public override void OnDestroy()
        {
            base.OnDestroy();

            Arguments.Dispose();
        }
    }
}