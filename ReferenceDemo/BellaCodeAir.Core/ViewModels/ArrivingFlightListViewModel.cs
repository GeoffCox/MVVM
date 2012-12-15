namespace BellaCodeAir.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    // TODO: #04 - A derived view model
    // --------------------------------------------------------------------------------
    /*
     * ArrivingFlightListViewModel derives from FlightListViewModel to set the sorting and filter for arriving flights.
     * 
     * ----- XAML -----
     * (Open BellaCodeAir/Styles/Flights.xaml and review the Flights.ArrivingFlights style)
     * The DataContext for this style is expected to be an ObservableCollection<Flight>
     * The view creates an ArrivingFlightListViewModel using the ViewModelScope element.
     * The view uses an event trigger to animate whenever the FlightAdded event is raised by the view model.
     * The view binds to the Flights property in the ListBox.
     * 
     * (Review the Flights.View style)
     * This view creates the arriving and departing views using a standard Control element with the associated style.
     * Each style uses a different view model to properly sort and filter the arriving vs. departing flights.  
    */

    public sealed class ArrivingFlightListViewModel : FlightListViewModel
    {
        private IFlightData _flightData;

        public ArrivingFlightListViewModel(IFlightData flightData)
        {
            if (flightData == null)
            {
                throw new ArgumentNullException("flightData");
            }

            this._flightData = flightData;

            this._flightsViewSource.SortDescriptions.Insert(0, new SortDescription("ArrivalDateTime", ListSortDirection.Ascending));
        }

        protected override bool FilterFlight(Models.Flight flight)
        {
            return this._flightData.HomeAirport == flight.Destination;
        }
    }
}
