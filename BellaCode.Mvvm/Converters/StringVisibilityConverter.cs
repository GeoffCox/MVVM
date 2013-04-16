namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows;

    /// <summary>
    /// Converts a string to a Visibility based on if the string is null/empty, or not empty.
    /// </summary>
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringVisibilityConverter : IValueConverter
    {
        public StringVisibilityConverter()
        {
            this.WhenNotEmpty = Visibility.Visible;
            this.WhenNullOrEmpty = Visibility.Hidden;
        }

        public Visibility WhenNotEmpty { get; set; }

        public Visibility WhenNullOrEmpty { get; set; }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string source = (string)value;

            if (!string.IsNullOrEmpty(source))
            {
                return this.WhenNotEmpty;
            }
            else
            {
                return this.WhenNullOrEmpty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
