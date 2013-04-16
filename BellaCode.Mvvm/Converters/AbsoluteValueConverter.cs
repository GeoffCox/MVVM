namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Windows.Data;

    /// <summary>
    /// Converts a number to its absolute value.
    /// </summary>
    /// <remarks>
    /// Set the WhenNull property to return a value other than null when the source is null.
    /// </remarks>
    [ValueConversion(typeof(int), typeof(int))]
    [ValueConversion(typeof(double), typeof(double))]
    [ValueConversion(typeof(decimal), typeof(decimal))]
    public class AbsoluteValueConverter : IValueConverter
    {
        public AbsoluteValueConverter()
        {
            this.WhenNull = null;
        }

        public decimal? WhenNull { get; set; }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var intValue = value as int?;

            if (intValue.HasValue)
            {
                return Math.Abs(intValue.Value);
            }

            var doubleValue = value as double?;

            if (doubleValue.HasValue)
            {
                return Math.Abs(doubleValue.Value);
            }

            var decimalValue = value as decimal?;

            if (decimalValue.HasValue)
            {
                return Math.Abs(decimalValue.Value);
            }

            return WhenNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
