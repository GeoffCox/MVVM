namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts a boolean to a Visibility based on if the boolen is null, false, or true.
    /// </summary>
    /// <remarks>
    /// This converter provides finer control over the conversion that the built-in boolean-visiblity converter.
    /// </remarks>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    [ValueConversion(typeof(bool?), typeof(Visibility))]
    public class BooleanVisibilityConverter : IValueConverter
    {
        public BooleanVisibilityConverter()
        {
            this.WhenTrue = Visibility.Visible;
            this.WhenFalse = Visibility.Hidden;
            this.WhenNull = Visibility.Hidden;
        }

        public Visibility WhenTrue { get; set; }

        public Visibility WhenFalse { get; set; }

        public Visibility WhenNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var source = (bool?)value;

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
            var source = value as Visibility?;

            if (source == this.WhenTrue)
            {
                return true;
            }

            if (source == this.WhenFalse)
            {
                return false;
            }

            if (source == this.WhenNull)
            {
                return null;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}

