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

using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace InstagroomXA.Droid.Presenters
{
    /// <summary>
    /// Custom view presenter for cleaning the navigation stack after displaying the requested view
    /// </summary>
    public class ClearStackCustomPresenter : MvxAndroidViewPresenter
    {
        public override void Show(MvxViewModelRequest request)
        {
            if (request != null && request.PresentationValues != null)
            {

                if (request.PresentationValues.ContainsKey("ClearStack"))
                {
                    // Get intent from request and set flags to clear backstack.
                    var intent = base.CreateIntentForRequest(request);
                    intent.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);

                    base.Show(intent);
                    return;
                }
            }

            base.Show(request);
        }
    }
}