using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design;

using InstagroomXA.Core.ViewModels;
using InstagroomXA.Droid.Views;

using MvvmCross.Droid.Views;
using MvvmCross.Droid.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V4;
using System.Threading.Tasks;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "Instagroom")]
    public class MasterTabControlView : MvxCachingFragmentActivity<MasterTabControlViewModel>
    {
        Toolbar masterToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MasterTabControlView);
            masterToolbar = FindViewById<Toolbar>(Resource.Id.masterToolbar);
            SetActionBar(masterToolbar);

            SetMenuStyle();
            
            LoadFragment(Resource.Id.menu_feed);
            // ViewModel.ShowInitialViewModelsCommand.Execute();
        }

        /// <summary>
        /// Disable back navigation
        /// </summary>
        public override void OnBackPressed()
        {
            return;
        }

        /// <summary>
        /// Sets the bottom navigation tab control style
        /// </summary>
        private void SetMenuStyle()
        {
            var tabControl = FindViewById<Android.Support.Design.Widget.BottomNavigationView>(Resource.Id.masterBottomNavigationView);
            tabControl.InflateMenu(Resource.Drawable.masterBottomTabMenu);

            tabControl.NavigationItemSelected += TabControl_NavigationItemSelected;
        }

        /// <summary>
        /// Change fragments on tab change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_NavigationItemSelected(object sender, 
            Android.Support.Design.Widget.BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        /// <summary>
        /// Instatiates a fragment with its viewmodel by ID & adds it into the content frame
        /// </summary>
        /// <param name="id"></param>
        private void LoadFragment(int id)
        {
            string tag = "instagroomxa.droid.views.";
            Android.Support.V4.App.Fragment fragment = null;

            switch (id)
            {
                case Resource.Id.menu_feed:
                    tag += "FeedView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ViewModel.FeedVM.Start();
                    ((MvxFragment)fragment).DataContext = ViewModel.FeedVM;

                    masterToolbar.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.menu_search:
                    tag += "SearchView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ((MvxFragment)fragment).DataContext = ViewModel.SearchVM;

                    masterToolbar.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.menu_newPost:
                    tag += "NewPostView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ((MvxFragment)fragment).DataContext = ViewModel.NewPostVM;

                    masterToolbar.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.menu_notifs:
                    tag += "NotificationsView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ((MvxFragment)fragment).DataContext = ViewModel.NotificationsVM;

                    masterToolbar.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.menu_profile:
                    tag += "ProfileView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ViewModel.ProfileVM.Start();
                    ((MvxFragment)fragment).DataContext = ViewModel.ProfileVM;

                    masterToolbar.Visibility = ViewStates.Visible;
                    ActionBar.Title = "Profile info";
                    break;
            }

            if (fragment == null) { return; }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment, tag)
                .Commit();
        }

        /// <summary>
        /// Inflates the menu for the profile info toolbar
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Drawable.profileInfoToolbarMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}