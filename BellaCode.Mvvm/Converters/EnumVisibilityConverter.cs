namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts an enum value to a Visibility by comparing the value to the converter parameter value.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(Visibility))]
    public class EnumVisibilityConverter : IValueConverter
    {
        public EnumVisibilityConverter()
        {
            this.VisibilityWhenEqual = Visibility.Visible;
            this.VisibilityWhenNotEqual = Visibility.Hidden;
        }

        public Visibility VisibilityWhenEqual { get; set; }

        public Visibility VisibilityWhenNotEqual { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Enum.Equals(value, parameter)) ? this.VisibilityWhenEqual : this.VisibilityWhenNotEqual;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("EnumVisibilityConverter can not convert visibility back to the original enumeration value.");
        }
    }

}
