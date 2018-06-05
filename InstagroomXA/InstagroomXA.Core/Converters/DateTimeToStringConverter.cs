using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Platform.Converters;

namespace InstagroomXA.Core.Converters
{
    public class DateTimeToStringConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return $"{value.DayOfWeek}, {value.ToString("MMM dd, yyyy hh:mm tt")}";
        }
    }
}
