namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts a boolean to its inverted value.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    [ValueConversion(typeof(bool?), typeof(bool?))]
    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Invert(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Invert(value);
        }

        private static object Invert(object value)
        {
            var toInvert = value as bool?;
            if (toInvert.HasValue)
            {
                return (!toInvert.Value);
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }

}
