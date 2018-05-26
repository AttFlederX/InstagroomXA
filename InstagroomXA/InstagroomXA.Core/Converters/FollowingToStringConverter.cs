using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Platform.Converters;

namespace InstagroomXA.Core.Converters
{
    /// <summary>
    /// Converts the list of following into the # of following string
    /// </summary>
    public class FollowingToStringConverter : MvxValueConverter<List<int>, string>
    {
        protected override string Convert(List<int> value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // return $"{value.Count} following";
            return value.Count.ToString();
        }
    }
}
