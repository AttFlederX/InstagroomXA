using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design;
using Android.Preferences;

using InstagroomXA.Core.ViewModels;
using InstagroomXA.Droid.Views;
using InstagroomXA.Core.Helpers;

using MvvmCross.Droid.Views;
using MvvmCross.Droid.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V4;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "Instagroom", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MasterTabControlView : MvxCachingFragmentActivity<MasterTabControlViewModel>
    {
        int _masterTabIdx;
        bool _isUserSaved;
        Toolbar _masterToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (!_isUserSaved)
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutInt(ConstantHelper.UserIDKey, ViewModel.CurrentUserID);

                editor.Apply();
            }

            // Create your application here
            SetContentView(Resource.Layout.MasterTabControlView);
            _masterToolbar = FindViewById<Toolbar>(Resource.Id.masterToolbar);
            SetActionBar(_masterToolbar);

            SetMenuStyle();

            if (savedInstanceState != null) { RestoreState(savedInstanceState); }
            else { _masterTabIdx = Resource.Id.menu_feed; }

            LoadFragment(_masterTabIdx);
            // ViewModel.ShowInitialViewModelsCommand.Execute();
        }

        /// <summary>
        /// Restores the activity state retrieved from a bundle
        /// </summary>
        /// <param name="savedInstanceState"></param>
        private void RestoreState(Bundle savedInstanceState)
        {
            _masterTabIdx = savedInstanceState.GetInt(ConstantHelper.AndroidTabIdxBundleKey);
            _masterToolbar.Visibility = (ViewStates)savedInstanceState.GetInt(ConstantHelper.AndroidProfileToolbarState);
        }

        ///// <summary>
        ///// Disable back navigation
        ///// </summary>
        //public override void OnBackPressed()
        //{
        //    return;
        //}

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
            _masterTabIdx = id;

            switch (id)
            {
                case Resource.Id.menu_feed:
                    tag += "FeedView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ViewModel.FeedVM.Start();
                    ((MvxFragment)fragment).DataContext = ViewModel.FeedVM;

                    _masterToolbar.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.menu_search:
                    tag += "SearchView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ((MvxFragment)fragment).DataContext = ViewModel.SearchVM;

                    _masterToolbar.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.menu_newPost:
                    tag += "NewPostView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ((MvxFragment)fragment).DataContext = ViewModel.NewPostVM;

                    _masterToolbar.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.menu_notifs:
                    tag += "NotificationsView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ViewModel.NotificationsVM.Start();
                    ((MvxFragment)fragment).DataContext = ViewModel.NotificationsVM;

                    _masterToolbar.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.menu_profile:
                    tag += "ProfileView";
                    fragment = Android.Support.V4.App.Fragment.Instantiate(this, tag);
                    ViewModel.ProfileVM.Start();
                    ((MvxFragment)fragment).DataContext = ViewModel.ProfileVM;

                    _masterToolbar.Visibility = ViewStates.Visible;
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

        /// <summary>
        /// Menu item click handler
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_signout)
            {
                // remove the saved user ID
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutInt(ConstantHelper.UserIDKey, -1);

                editor.Apply();

                ViewModel.SignOutCommand.Execute();

                return true;
            }
            else { return base.OnOptionsItemSelected(item); }
        }

        /// <summary>
        /// Saves the activity state into a bundle
        /// </summary>
        /// <param name="outState"></param>
        /// <param name="outPersistentState"></param>
        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            outState.PutInt(ConstantHelper.AndroidTabIdxBundleKey, _masterTabIdx);
            outState.PutInt(ConstantHelper.AndroidProfileToolbarState, (int)_masterToolbar.Visibility);

            base.OnSaveInstanceState(outState, outPersistentState);
        }
    }
}