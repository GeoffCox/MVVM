namespace BellaCodeAir.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    public sealed class DepartingFlightListViewModel : FlightListViewModel
    {
        private IFlightData _flightData;

        public DepartingFlightListViewModel(IFlightData flightData)
        {
            if (flightData == null)
            {
                throw new ArgumentNullException("flightData");
            }

            this._flightData = flightData;

            this._flightsViewSource.SortDescriptions.Insert(0, new SortDescription("DepartureDateTime", ListSortDirection.Ascending));
        }

        protected override bool FilterFlight(Models.Flight flight)
        {
            return this._flightData.HomeAirport == flight.Origin;
        }
    }
}
