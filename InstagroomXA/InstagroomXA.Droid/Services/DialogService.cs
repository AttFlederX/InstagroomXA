using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using InstagroomXA.Core.Contracts;

using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace InstagroomXA.Droid.Services
{
    /// <summary>
    /// Android implementation of the dialog service
    /// </summary>
    public class DialogService : IDialogService
    {
        protected Activity CurrentActivity =>
            Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

        public Task ShowAlertAsync(string message,
            string title, string buttonText)
        {
            return Task.Run(() =>
            {
                Alert(message, title, buttonText);
            });
        }

        private void Alert(string message, string title, string okButton)
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                var builder = new AlertDialog.Builder(CurrentActivity);
                builder.SetIconAttribute
                    (Android.Resource.Attribute.AlertDialogIcon);
                builder.SetTitle(title);
                builder.SetMessage(message);
                builder.SetPositiveButton(okButton, delegate { });
                builder.Create().Show();
            }, null);
        }
    }
}