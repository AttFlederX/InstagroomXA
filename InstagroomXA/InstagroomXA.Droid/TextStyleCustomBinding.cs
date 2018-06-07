using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;

namespace InstagroomXA.Droid
{
    /// <summary>
    /// Custom MVX binding for text styles (e.g. bold, italic)
    /// </summary>
    public class TextStyleCustomBinding : MvxAndroidTargetBinding
    {
        private readonly TextView _textView;

        public TextStyleCustomBinding(TextView textView) : base(textView)
        {
            _textView = textView;
        }

        protected override void SetValueImpl(object target, object value)
        {
            _textView.SetTypeface(_textView.Typeface, (Android.Graphics.TypefaceStyle)value);
        }

        public override Type TargetType
        {
            get { return typeof(Android.Graphics.TypefaceStyle); }
        }

        public override MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.OneWay; }
        }
    }
}