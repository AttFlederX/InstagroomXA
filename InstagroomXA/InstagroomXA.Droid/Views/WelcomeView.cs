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
using Android.Graphics;
using Android.Content.PM;
using Android.Preferences;

using MvvmCross.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;

using InstagroomXA.Core.ViewModels;
using InstagroomXA.Core.Helpers;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "Instagroom", ScreenOrientation = ScreenOrientation.Portrait)]
    public class WelcomeView : MvxActivity<WelcomeViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Auto-login
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            int uid = prefs.GetInt(ConstantHelper.UserIDKey, -1);
            if (uid != -1) { ViewModel.AutoLoginCommand.Execute(uid); }
            else
            {
                // Create your application here
                SetContentView(Resource.Layout.WelcomeView);

                SetViewFonts();
            }
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