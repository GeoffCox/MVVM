namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts a boolean to a known value based on if the boolean is null, false, or true.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(object))]
    [ValueConversion(typeof(bool?), typeof(object))]
    public class BooleanToKnownValueConverter : IValueConverter
    {
        public object WhenTrue { get; set; }

        public object WhenFalse { get; set; }

        public object WhenNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? source = (bool?)value;

            if (source.HasValue)
            {
                return (source.Value == true) ? this.WhenTrue : this.WhenFalse;
            }
            else
            {
                return this.WhenNull;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == this.WhenTrue)
            {
                return true;
            }

            if (value == this.WhenFalse)
            {
                return false;
            }

            if (value == this.WhenNull)
            {
                return null;
            }

            return DependencyProperty.UnsetValue;
        }
    }

}
