﻿using System;
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
    [Activity(Label = "Registration")]
    public class RegistrationView : MvxActivity<RegistationViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RegistrationView);
        }
    }
}