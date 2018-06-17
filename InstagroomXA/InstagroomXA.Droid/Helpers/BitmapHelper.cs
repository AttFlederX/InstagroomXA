using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace InstagroomXA.Droid.Helpers
{
    public static class BitmapHelper
    {
        public static Bitmap LoadAndResizeBitmap(string fileName, int width, int height)
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

            return FixOrientation(BitmapFactory.DecodeFile(fileName, bmOptions), fileName);
        }

        public static void SaveImageThumbnail(Bitmap bitmap, string savePath)
        {
            using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            }
        }


        private static Bitmap FixOrientation(Bitmap bitmap, string fileName)
        {
            ExifInterface exif = new ExifInterface(fileName);
            int orientation = exif.GetAttributeInt(ExifInterface.TagOrientation, 1);

            int rotationDegrees = 0;
            switch (orientation)
            {
                case 6:
                    rotationDegrees = 90;
                    break;
                case 3:
                    rotationDegrees = 180;
                    break;
                case 8:
                    rotationDegrees = 270;
                    break;
            }

            Matrix matrix = new Matrix();
            matrix.PostRotate(rotationDegrees);
            return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
        }
    }
}