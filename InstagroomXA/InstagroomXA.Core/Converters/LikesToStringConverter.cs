using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Platform.Converters;

namespace InstagroomXA.Core.Converters
{
    /// <summary>
    /// Converts the # of likes into the # of likes string
    /// </summary>
    public class LikesToStringConverter : MvxValueConverter<int, string>
    {
        protected override string Convert(int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return $"{value} likes";
        }
    }
}
