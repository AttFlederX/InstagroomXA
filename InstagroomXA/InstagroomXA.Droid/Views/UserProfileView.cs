using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using InstagroomXA.Core.Helpers;
using InstagroomXA.Core.ViewModels;

using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "User profile")]
    public class UserProfileView : MvxActivity<UserProfileViewModel>
    {
        private Button _followButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UserProfileView);

            var postsRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.userPostsRecyclerView);
            if (postsRecyclerView != null)
            {
                postsRecyclerView.HasFixedSize = true;
                var layoutManager = new GridLayoutManager(this, ConstantHelper.AndroidProfilePostsRecyclerViewColumnNum);
                postsRecyclerView.SetLayoutManager(layoutManager);
            }

            _followButton = FindViewById<Button>(Resource.Id.followButton);
            _followButton.Click += FollowButton_Click;

            UpdateFollowButtonAppearance();
        }

        private void FollowButton_Click(object sender, EventArgs e)
        {
            ViewModel.FollowCommand.Execute();
            UpdateFollowButtonAppearance();
        }


        private void UpdateFollowButtonAppearance()
        {
            if (ViewModel.AppCurrentUser.Following.Contains(ViewModel.UserId))
            {
                _followButton.SetTypeface(_followButton.Typeface, Android.Graphics.TypefaceStyle.Bold);
                _followButton.Text = GetString(Resource.String.followedButtonText);
            }
            else
            {
                _followButton.SetTypeface(_followButton.Typeface, Android.Graphics.TypefaceStyle.Normal);
                _followButton.Text = GetString(Resource.String.followButtonText);
            }
        }
    }
}