using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Platform.Converters;

namespace InstagroomXA.Core.Converters
{
    /// <summary>
    /// Converts the list of followers into the # of followers string
    /// </summary>
    public class FollowersToStringConverter : MvxValueConverter<List<int>, string>
    {
        protected override string Convert(List<int> value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // return $"{value.Count} followers";
            return value.Count.ToString();
        }
    }
}
