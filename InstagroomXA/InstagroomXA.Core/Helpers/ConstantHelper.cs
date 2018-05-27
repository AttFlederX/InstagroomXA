﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagroomXA.Core.Helpers
{
    /// <summary>
    /// Helper class for storing constants
    /// </summary>
    public class ConstantHelper
    {
        public static string ImageDirectoryName { get => "instxa"; }

        public enum AndroidCameraRequestCodes { Camera, Gallery }
    }
}