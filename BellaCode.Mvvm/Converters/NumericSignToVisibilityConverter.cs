namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows;

    /// <summary>
    /// Converts a number to a Visibility based on if the number is null, negative, zero, or positive.
    /// </summary>
    /// <remarks>
    /// The default is that all non-null values are visible.
    /// </remarks>
    [ValueConversion(typeof(int), typeof(bool))]
    [ValueConversion(typeof(double), typeof(bool))]
    [ValueConversion(typeof(decimal), typeof(bool))]
    public class NumericSignToVisibilityConverter : IValueConverter
    {
        public NumericSignToVisibilityConverter()
        {
            this.WhenPositive = Visibility.Visible;
            this.WhenNegative = Visibility.Visible;
            this.WhenZero = Visibility.Visible;
            this.WhenNull = Visibility.Hidden;
        }

        public Visibility WhenPositive { get; set; }

        public Visibility WhenNegative { get; set; }

        public Visibility WhenZero { get; set; }

        public Visibility WhenNull { get; set; }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var intValue = value as int?;

            if (intValue.HasValue)
            {
                if (intValue.Value == 0)
                {
                    return this.WhenZero;
                }

                return (intValue.Value > 0) ? this.WhenPositive : this.WhenNegative;
            }

            var doubleValue = value as double?;

            if (doubleValue.HasValue)
            {
                if (doubleValue.Value == 0)
                {
                    return this.WhenZero;
                }

                return (doubleValue.Value > 0) ? this.WhenPositive : this.WhenNegative;
            }

            var decimalValue = value as decimal?;

            if (decimalValue.HasValue)
            {
                if (decimalValue.Value == 0)
                {
                    return this.WhenZero;
                }

                return (decimalValue.Value > 0) ? this.WhenPositive : this.WhenNegative;
            }

            return WhenNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
