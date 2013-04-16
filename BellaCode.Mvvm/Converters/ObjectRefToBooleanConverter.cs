namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Windows.Data;

    /// <summary>
    /// Converts an object reference to a boolean based on if the object is null or not null.
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class ObjectRefToBooleanConverter : IValueConverter
    {
        public ObjectRefToBooleanConverter()
        {
            WhenNotNull = true;
            WhenNull = false;
        }

        public bool WhenNotNull { get; set; }

        public bool WhenNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value != null) ? WhenNotNull : WhenNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("ObjectRefToBooleanConverter can not convert a boolean back to the original object.");
        }
    }

}
