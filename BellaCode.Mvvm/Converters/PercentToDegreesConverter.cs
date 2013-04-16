namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows;

    /// <summary>
    /// Converts a number from a percentage (0-100) to a number of degrees (0-360).
    /// </summary>
    [ValueConversion(typeof(int), typeof(int))]
    public class PercentToDegreesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? source = value as int?;
            if (source.HasValue)
            {
                return source * 3.60;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? source = value as int?;
            if (source.HasValue)
            {
                return source / 3.60;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
