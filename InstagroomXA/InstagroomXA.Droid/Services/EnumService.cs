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

using InstagroomXA.Core.Contracts;

namespace InstagroomXA.Droid.Services
{
    /// <summary>
    /// Android implementation of the enum service
    /// </summary>
    public class EnumService : IEnumService
    {
        public int ViewStateVisible { get => (int)ViewStates.Visible; }
        public int ViewStateInvisible { get => (int)ViewStates.Invisible; }
        public int ViewStateGone { get => (int)ViewStates.Gone; }

        public int TypefaceStyleNormal { get => (int)TypefaceStyle.Normal; }
        public int TypefaceStyleBold { get => (int)TypefaceStyle.Bold; }
        public int TypefaceStyleItalic { get => (int)TypefaceStyle.Italic; }
        public int TypefaceStyleBoldItalic { get => (int)TypefaceStyle.BoldItalic; }
    }
}