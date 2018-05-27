using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace InstagroomXA.Droid.Helpers
{
    public static class BitmapHelper
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            int photoWidth = 0;
            int photoHeight = 0;

            // Get the dimensions of the bitmap
            BitmapFactory.Options bmOptions = new BitmapFactory.Options();
            bmOptions.InJustDecodeBounds = true;
            BitmapFactory.DecodeFile(fileName, bmOptions);
            photoWidth = bmOptions.OutWidth;
            photoHeight = bmOptions.OutHeight;

            // Determine how much to scale down the image
            int scaleFactor = Math.Min(photoWidth / width, photoHeight / height);

            // Decode the image file into a Bitmap sized to fill the View
            bmOptions.InJustDecodeBounds = false;
            bmOptions.InSampleSize = scaleFactor;
            bmOptions.InPurgeable = true;

            return BitmapFactory.DecodeFile(fileName, bmOptions);
        }
    }
}