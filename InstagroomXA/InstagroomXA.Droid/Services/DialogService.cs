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

        public async Task ShowAlertAsync(string message, string title, string buttonText, Action okCallback = null)
        {
            await Task.Run(() =>
            {
                Alert(message, title, buttonText, okCallback);
            });
        }

        public void ShowPopupMessage(string message)
        {
            var toast = Toast.MakeText(Application.Context, message, ToastLength.Long);
            toast.Show();
        }


        private void Alert(string message, string title, string okButton, Action okCallback = null)
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                var builder = new AlertDialog.Builder(CurrentActivity);
                builder.SetIconAttribute
                    (Android.Resource.Attribute.AlertDialogIcon);
                builder.SetTitle(title);
                builder.SetMessage(message);
                builder.SetPositiveButton(okButton, delegate { if (okCallback != null) { okCallback(); } });
                builder.Create().Show();
            }, null);
        }
    }
}