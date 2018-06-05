using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Database;

using InstagroomXA.Core.Helpers;
using InstagroomXA.Core.ViewModels;
using InstagroomXA.Droid.Helpers;
using InstagroomXA.Droid.Extensions;

using Java.IO;

using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;

namespace InstagroomXA.Droid.Views
{
    [MvxFragment(typeof(MasterTabControlViewModel), Resource.Id.content_frame, true)]
    [Register("instagroomxa.droid.views.NewPostView")]
    public class NewPostView : MvxFragment<NewPostViewModel>
    {
        private ImageView _postImageView;
        private Button _takePhotoButton;
        private Button _openGalleryButton;
        private Button _postButton;

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
            var view = this.BindingInflate(Resource.Layout.NewPostView, null);

            SetImageDirectory();

            _postImageView = view.FindViewById<ImageView>(Resource.Id.postImageView);
            _takePhotoButton = view.FindViewById<Button>(Resource.Id.takePhotoButton);
            _openGalleryButton = view.FindViewById<Button>(Resource.Id.openGalleryButton);
            _postButton = view.FindViewById<Button>(Resource.Id.postButton);

            _takePhotoButton.Click += TakePhotoButton_Click;
            _openGalleryButton.Click += OpenGalleryButton_Click;
            _postButton.Click += PostButton_Click;

            //if (CameraDataHelper.CurrentImage != null)
            //{
            //    SetImage();
            //}

            return view;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == (int)ConstantHelper.AndroidCameraRequestCodes.Camera)
            {
                if (resultCode == (int)Result.Ok)
                {
                    SetImage();
                }
                CameraDataHelper.CurrentImage = null;
            }
            else if (requestCode == (int)ConstantHelper.AndroidCameraRequestCodes.Gallery)
            {
                if (resultCode == (int)Result.Ok)
                {
                    CameraDataHelper.CurrentImage = new File(data.Data.GetRealPath(this.Context));
                    SetImage();
                }
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

        private void PostButton_Click(object sender, EventArgs e)
        {
            ViewModel.PostCommand.Execute();

            CameraDataHelper.CurrentImage = null;
            _postImageView.SetImageBitmap(null);
        }


        private void SetImage()
        {
            int width = _postImageView.Width;
            int height = _postImageView.Height;

            ViewModel.NewPost.ImagePath = CameraDataHelper.CurrentImage.Path;
            _postImageView.SetImageBitmap(BitmapHelper.LoadAndResizeBitmap(CameraDataHelper.CurrentImage.Path, width, height));

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