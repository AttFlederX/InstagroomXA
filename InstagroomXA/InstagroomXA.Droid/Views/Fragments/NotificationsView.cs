using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

using InstagroomXA.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace InstagroomXA.Droid.Views
{
    [MvxFragment(typeof(MasterTabControlViewModel), Resource.Id.content_frame, true)]
    [Register("instagroomxa.droid.views.NotificationsView")]
    public class NotificationsView : MvxFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.NotificationsView, null);

            var notificationsRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.notificationsRecyclerView);
            if (notificationsRecyclerView != null)
            {
                notificationsRecyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(Activity);
                notificationsRecyclerView.SetLayoutManager(layoutManager);
            }

            return view;
        }
    }
}