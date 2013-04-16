namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;

    /// <summary>
    /// Converts a number from a zero-based indexing to a one-based indexing.
    /// </summary>
    [ValueConversion(typeof(int), typeof(int))]
    public class ZeroToOneBasedIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? number = value as int?;
            return number.HasValue ? (number + 1) : value;
        }        

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? number = value as int?;
            return number.HasValue ? (number - 1) : value;
        }           
    }
}
