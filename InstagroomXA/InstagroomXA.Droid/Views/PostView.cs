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

using InstagroomXA.Core.ViewModels;

using MvvmCross.Droid.Views;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "Post")]
    public class PostView : MvxActivity<PostViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PostView);
        }
    }
}