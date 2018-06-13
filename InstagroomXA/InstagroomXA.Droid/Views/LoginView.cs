using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using MvvmCross.Droid.Views;

using InstagroomXA.Core.ViewModels;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "Log in", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : MvxActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LoginView);

            SetViewFonts();
        }

        /// <summary>
        /// Sets the fonts for text views
        /// To be later moved into AXML code
        /// </summary>
        private void SetViewFonts()
        {
            var logoView = FindViewById<TextView>(Resource.Id.logoTextView);

            logoView.SetTypeface(Typeface.CreateFromAsset(Assets, "font/Logo.ttf"), TypefaceStyle.Normal);
        }
    }
}