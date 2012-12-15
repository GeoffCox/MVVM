namespace BellaCodeAir.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows.Data;
    using BellaCode.Mvvm;
    using BellaCodeAir.Models;

    // TODO: #03 - A view model for a collection of models.
    // --------------------------------------------------------------------------------
    /*
     * FlightListViewModel is a ViewModel that takes a collection of Flight instances as its model.
     * It exposes the Flights property to provide the view a sorted and filtered collection.
     * It is build for derivation by the arriving and departing view models.
     * It provides a FlightAdded event for the UI when a flight is added that appears in the filtered Flights collection.          
    */

    public class FlightListViewModel : ViewModel<ObservableCollection<Flight>>
    {
        protected CollectionViewSource _flightsViewSource = new CollectionViewSource();
        private ICollectionView _flightsView;

        public FlightListViewModel()
        {
            this._flightsViewSource.SortDescriptions.Add(new SortDescription("AirlineCode", ListSortDirection.Ascending));
            this._flightsViewSource.SortDescriptions.Add(new SortDescription("Number", ListSortDirection.Ascending));

            this._flightsViewSource.Filter += new FilterEventHandler(Flights_Filter);
        }

        public ICollectionView Flights
        {
            get
            {
                return this._flightsView;
            }
            set
            {
                if (this._flightsView != value)
                {
                    this._flightsView = value;
                    this.RaisePropertyChanged("Flights");
                }
            }
        }

        public event EventHandler FlightAdded;

        private void RaiseFlightAdded()
        {
            if (this.FlightAdded != null)
            {
                this.FlightAdded(this, EventArgs.Empty);
            }
        }

        protected override void OnModelChanged(ObservableCollection<Flight> oldValue, ObservableCollection<Flight> newValue)
        {
            base.OnModelChanged(oldValue, newValue);

            if (oldValue != null)
            {
                oldValue.CollectionChanged -= this.FlightsSource_CollectionChanged;
            }

            if (newValue != null)
            {
                newValue.CollectionChanged += this.FlightsSource_CollectionChanged;
            }

            this.UpdateFlights();
        }

        private void FlightsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null && e.NewItems.Count > 0)
            {
                var flight = e.NewItems[0] as Flight;
                if (FilterFlight(flight))
                {
                    this.RaiseFlightAdded();
                }
            }
        }

        private void UpdateFlights()
        {
            this._flightsViewSource.Source = this.Model;
            this.Flights = this._flightsViewSource.View;
        }

        private void Flights_Filter(object sender, FilterEventArgs e)
        {
            Flight flight = e.Item as Flight;
            e.Accepted = (flight != null) && FilterFlight(flight);
        }

        protected virtual bool FilterFlight(Flight flight)
        {            
            return true;
        }
    }
}
