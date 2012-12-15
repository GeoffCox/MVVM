namespace BellaCodeAir.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using BellaCodeAir.ViewModels;

    [ValueConversion(typeof(FlightStatus),typeof(string))]
    public class FlightStatusValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var flightStatus = value as FlightStatus?;

            if (flightStatus.HasValue)
            {
                switch (flightStatus.Value)
                {
                    case FlightStatus.WaitingForDeparture:
                        return "Waiting For Departure";
                    case FlightStatus.InFlight:
                        return "In Flight";
                    case FlightStatus.Arrived:
                        return "Arrived";
                }
            }

            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
