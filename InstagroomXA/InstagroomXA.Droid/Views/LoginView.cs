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

using MvvmCross.Droid.Views;

using InstagroomXA.Core.ViewModels;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "Log in")]
    public class LoginView : MvxActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
            }
            catch (Exception ex)
            {
                try
                {
                    throw ex.InnerException;
                }
                catch (Exception iex)
                {
                    throw iex.InnerException;
                }
            }


            // Create your application here
            SetContentView(Resource.Layout.LoginView);
        }
    }
}