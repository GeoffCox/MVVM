namespace BellaCode.Mvvm.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows;

    /// <summary>
    /// Converts a number to oa Visibility based on if the number is null, inside the range, or outside the range.
    /// </summary>
    /// <remarks>
    /// The lower and upper bound properties are inclusive.
    /// </remarks>
    [ValueConversion(typeof(double), typeof(Visibility))]
    public class NumericRangeVisibilityConverter : IValueConverter
    {
        public NumericRangeVisibilityConverter()
        {
            this.WhenNull = Visibility.Hidden;
            this.WhenInsideRange = Visibility.Visible;
            this.WhenOutsideRange = Visibility.Hidden;
        }

        /// <summary>
        /// Gets or sets the visibility when the value is null or NaN.  Defaults to Visibility.Hidden.
        /// </summary>        
        public Visibility WhenNull { get; set; }

        /// <summary>
        /// Gets or sets the visibility when the value is inside the range.  Defaults to Visibility.Visible.
        /// </summary>        
        public Visibility WhenInsideRange { get; set; }

        /// <summary>
        /// Gets or sets the visibility when the value is outside the range.  Defaults to Visibility.Hidden.
        /// </summary>        
        public Visibility WhenOutsideRange { get; set; }

        /// <summary>
        /// Gets or sets the lower bound (inclusive).
        /// </summary>
        public double? LowerBound { get; set; }

        /// <summary>
        /// Gets or sets the upper bound (inclusive).
        /// </summary>
        public double? UpperBound { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = value.ToString();

            if (string.IsNullOrEmpty(text))
            {
                return this.WhenNull;
            }

            double number;
            if (!double.TryParse(text, out number))
            {
                return this.WhenNull;
            }

            bool isBelowLowerBound = (this.LowerBound.HasValue) ? (number < this.LowerBound.Value) : false;
            bool isAboveUpperBound = (this.UpperBound.HasValue) ? (number > this.UpperBound.Value) : false;
                
            if (isBelowLowerBound || isAboveUpperBound)
            {
                return this.WhenOutsideRange;
            }

            return this.WhenInsideRange;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
