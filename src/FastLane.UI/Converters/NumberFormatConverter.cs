using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FastLane.UI.Converters
{
    /// <summary>
    /// Formats numeric values into strings with grouping separators and decimals.
    /// </summary>
    public class NumberFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return doubleValue.ToString("N2", culture);
            }
            if (value is int intValue)
            {
                return intValue.ToString("N0", culture);
            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && double.TryParse(str, NumberStyles.Any, culture, out double result))
            {
                if (targetType == typeof(int))
                {
                    return (int)result;
                }
                return result;
            }
            return 0;
        }
    }
}
