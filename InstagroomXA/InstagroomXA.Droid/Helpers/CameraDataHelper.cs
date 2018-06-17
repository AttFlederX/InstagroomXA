using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Java.IO;

namespace InstagroomXA.Droid.Helpers
{
    /// <summary>
    /// Helper class for storing current camera image data
    /// </summary>
    public class CameraDataHelper
    {
        public static File CurrentImage { get; set; }
        public static File ImageDirectory { get; set; }
        public static File ThumbnailDirectory { get; set; }
    }
}