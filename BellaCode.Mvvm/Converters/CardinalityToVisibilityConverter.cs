namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows;

    /// <summary>
    /// Converts a number to a Visibility based on if the number is null, zero, one, or greater than one. 
    /// </summary>
    /// <remarks>
    /// The default is Visible when greater than zero.
    /// </remarks>
    [ValueConversion(typeof(int), typeof(bool))]
    [ValueConversion(typeof(double), typeof(bool))]
    [ValueConversion(typeof(decimal), typeof(bool))]
    public class CardinalityToVisibilityConverter : IValueConverter
    {
        public CardinalityToVisibilityConverter()
        {
            this.WhenOne = Visibility.Visible;
            this.WhenMany = Visibility.Visible;
            this.WhenZero = Visibility.Hidden;
            this.WhenNull = Visibility.Hidden;
        }

        public Visibility WhenOne { get; set; }

        public Visibility WhenMany { get; set; }

        public Visibility WhenZero { get; set; }

        public Visibility WhenNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var intValue = value as int?;

            if (intValue != null)
            {
                if (intValue.Value == 0)
                {
                    return this.WhenZero;
                }

                return (intValue.Value > 1) ? this.WhenMany : this.WhenOne;
            }

            var doubleValue = value as double?;

            if (doubleValue != null)
            {
                if (doubleValue.Value == 0)
                {
                    return this.WhenZero;
                }

                return (doubleValue.Value > 1) ? this.WhenMany : this.WhenOne;
            }

            var decimalValue = value as decimal?;

            if (decimalValue != null)
            {
                if (decimalValue.Value == 0)
                {
                    return this.WhenZero;
                }

                return (decimalValue.Value > 1) ? this.WhenMany : this.WhenOne;
            }

            return WhenNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
