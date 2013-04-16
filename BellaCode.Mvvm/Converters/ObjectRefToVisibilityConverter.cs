namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts an object reference to a Visibility based on if the object is null or not null.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class ObjectRefVisibilityConverter : IValueConverter
    {
        public ObjectRefVisibilityConverter()
        {
            this.WhenNotNull = Visibility.Visible;
            this.WhenNull = Visibility.Hidden;
        }

        public Visibility WhenNotNull { get; set; }

        public Visibility WhenNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value != null) ? this.WhenNotNull : this.WhenNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("ObjectRefVisibilityConverter can not convert visiblity back to the original object.");
        }
    }

}
