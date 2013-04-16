namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Windows.Data;

    /// <summary>
    /// Converts an enum value to a boolean by comparing the enum value to the converter parameter value.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enum.Equals(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("EnumBooleanConverter can not convert a boolean back to the original enumeration value.");
        }
    }

}
