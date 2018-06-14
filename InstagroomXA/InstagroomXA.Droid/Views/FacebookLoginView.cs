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
using InstagroomXA.Core.Helpers;
using InstagroomXA.Core.ViewModels;
using Java.Lang;
using MvvmCross.Droid.Views;

using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Xamarin.Facebook.Login.Widget;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "Log in via Facebook", ScreenOrientation = ScreenOrientation.Portrait)]
    public class FacebookLoginView : MvxActivity<FacebookLoginViewModel>, IFacebookCallback
    {
        private FBProfileTracker _masterProfileTracker;
        private ICallbackManager _masterFBCallManager;

        private TextView _fullNameTextView;
        private LoginButton _facebookLoginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Facebook API initialization
            FacebookSdk.SdkInitialize(this.ApplicationContext);
            AppEventsLogger.ActivateApp(this.Application);

            _masterProfileTracker = new FBProfileTracker();
            _masterProfileTracker.mOnProfileChanged += MasterProfileTracker_mOnProfileChanged;
            _masterProfileTracker.StartTracking();

            // Create your application here
            SetContentView(Resource.Layout.FacebookLoginView);

            _fullNameTextView = FindViewById<TextView>(Resource.Id.fullNameTextView);
            _facebookLoginButton = FindViewById<LoginButton>(Resource.Id.facebookLoginButton);

            _facebookLoginButton.SetReadPermissions(new List<string> {
                "user_friends",
                "public_profile"
            });
            _masterFBCallManager = CallbackManagerFactory.Create();
            _facebookLoginButton.RegisterCallback(_masterFBCallManager, this);
        }


        public void OnCancel()
        {
            return;
        }

        public void OnError(FacebookException error)
        {
            return;
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            return;
        }

        private void MasterProfileTracker_mOnProfileChanged(object sender, ProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                try
                {
                    ViewModel.FullName = $"{e.mProfile.FirstName} {e.mProfile.LastName}";
                    ViewModel.Username = e.mProfile.Name.Replace(" ", "").ToLowerInvariant();
                    ViewModel.ImagePath = string.Format(ConstantHelper.FacebookProfilePictureLink, e.mProfile.Id);

                    ViewModel.NewUser = new Core.Model.User()
                    {
                        Email = string.Empty,
                        FacebookID = e.mProfile.Id,
                        FirstName = e.mProfile.FirstName,
                        ImagePath = ViewModel.ImagePath,
                        LastName = e.mProfile.LastName
                    };

                    ViewModel.LoginViaFacebookCommand.Execute();
                }
                catch (Java.Lang.Exception ex) { }
            }
            else
            {
                ViewModel.FullName = string.Empty;
                ViewModel.Username = string.Empty;
                ViewModel.ImagePath = string.Empty;

                ViewModel.NewUser = new Core.Model.User();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _masterFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }


    #region Facebook API classes
    public class FBProfileTracker : ProfileTracker
    {
        public event EventHandler<ProfileChangedEventArgs> mOnProfileChanged;
        protected override void OnCurrentProfileChanged(Profile oldProfile, Profile newProfile)
        {
            if (mOnProfileChanged != null)
            {
                mOnProfileChanged.Invoke(this, new ProfileChangedEventArgs(newProfile));
            }
        }
    }

    public class ProfileChangedEventArgs : EventArgs
    {
        public Profile mProfile;
        public ProfileChangedEventArgs(Profile profile)
        {
            mProfile = profile;
        }
    }
    #endregion
}