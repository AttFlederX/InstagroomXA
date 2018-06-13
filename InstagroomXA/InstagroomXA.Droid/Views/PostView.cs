using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using InstagroomXA.Core.ViewModels;

using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "Post", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PostView : MvxActivity<PostViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PostView);

            var postsRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.commentsRecyclerView);
            if (postsRecyclerView != null)
            {
                postsRecyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(this);
                postsRecyclerView.SetLayoutManager(layoutManager);
            }
        }
    }
}