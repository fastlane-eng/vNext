using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FastLane.UI.Converters
{
    /// <summary>
    /// Converts boolean values to visibility states for MAUI views.
    /// True returns true (visible), false returns false (hidden).
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool visibility)
            {
                return visibility;
            }
            return false;
        }
    }
}
