using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using InstagroomXA.Core.Helpers;
using InstagroomXA.Core.ViewModels;
using InstagroomXA.Droid.Extensions;
using InstagroomXA.Droid.Helpers;
using InstagroomXA.Droid.Services;
using Java.IO;

using MvvmCross.Droid.Views;

namespace InstagroomXA.Droid.Views
{
    [Activity(Label = "EditProfileView", ScreenOrientation = ScreenOrientation.Portrait)]
    public class EditProfileView : MvxActivity<EditProfileViewModel>
    {
        private ImageView _pInfoImageView;
        private Button _pInfoTakePhotoButton;
        private Button _pInfoOpenGalleryButton;
        private Button _saveChangesButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EditProfileView);

            SetViews();
            SetClickEvents();

            // _pInfoImageView.SetImageURI();
        }

        private void SetViews()
        {
            _pInfoImageView = FindViewById<ImageView>(Resource.Id.pInfoImageView);
            _pInfoTakePhotoButton = FindViewById<Button>(Resource.Id.pInfoTakePhotoButton);
            _pInfoOpenGalleryButton = FindViewById<Button>(Resource.Id.pInfoOpenGalleryButton);
            _saveChangesButton = FindViewById<Button>(Resource.Id.saveChangesButton);
        }

        private void SetClickEvents()
        {
            _pInfoTakePhotoButton.Click += TakePhotoButton_Click;
            _pInfoOpenGalleryButton.Click += OpenGalleryButton_Click;
            _saveChangesButton.Click += SaveChangesButton_Click;
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            try
            {
                if (requestCode == (int)ConstantHelper.AndroidCameraRequestCodes.Camera)
                {
                    if (resultCode == Result.Ok)
                    {
                        SetImage();
                    }
                    CameraDataHelper.CurrentImage = null;
                }
                else if (requestCode == (int)ConstantHelper.AndroidCameraRequestCodes.Gallery)
                {
                    if (resultCode == Result.Ok)
                    {
                        CameraDataHelper.CurrentImage = new File(data.Data.GetRealPath(this));
                        SetImage();
                    }
                }
            }
            catch (Exception ex)
            {
                (new DialogService()).ShowPopupMessage($"Failed to upload the image\n{ex.Message}");
            }
        }


        private void TakePhotoButton_Click(object sender, EventArgs e)
        {
            ViewModel.TakePhotoCommand.Execute();

            var intent = new Intent(MediaStore.ActionImageCapture);
            CameraDataHelper.CurrentImage = new File(CameraDataHelper.ImageDirectory,
                $"{ConstantHelper.ImageDirectoryName}_{Guid.NewGuid()}.jpg");
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(CameraDataHelper.CurrentImage));

            StartActivityForResult(intent, (int)ConstantHelper.AndroidCameraRequestCodes.Camera);
        }

        private void OpenGalleryButton_Click(object sender, EventArgs e)
        {
            ViewModel.OpenGalleryCommand.Execute();

            var intent = new Intent(Intent.ActionGetContent);
            intent.SetType("image/*");

            StartActivityForResult(Intent.CreateChooser(intent, "Select an image"), (int)ConstantHelper.AndroidCameraRequestCodes.Gallery);
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            ViewModel.SaveChangesCommand.Execute();

            CameraDataHelper.CurrentImage = null;
            _pInfoImageView.SetImageBitmap(null);
        }


        private void SetImage()
        {
            int width = _pInfoImageView.Width;
            int height = _pInfoImageView.Height;

            ViewModel.ImagePath = CameraDataHelper.CurrentImage.Path;
            _pInfoImageView.SetImageBitmap(BitmapHelper.LoadAndResizeBitmap(CameraDataHelper.CurrentImage.Path, width, height));

            GC.Collect();
        }

        private void SetImageDirectory()
        {
            CameraDataHelper.ImageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                Android.OS.Environment.DirectoryPictures), ConstantHelper.ImageDirectoryName);

            if (!CameraDataHelper.ImageDirectory.Exists()) { CameraDataHelper.ImageDirectory.Mkdirs(); }
        }
    }
}