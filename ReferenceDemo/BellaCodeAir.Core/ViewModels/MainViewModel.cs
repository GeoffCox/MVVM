namespace BellaCodeAir.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using BellaCode.Mvvm;
    using BellaCodeAir.Models;

    // TODO: #05 - A top-level view model
    // --------------------------------------------------------------------------------
    /*
     * MainViewModel is not provided a model.  Instead it uses the data source to provide properties to the view.
     * The Flights property is initialized with a randomly generated set of flights.
     * The AddFlight, DeleteFlight, and Reset methods allow the view to change the list of Flights.
     * The CanDeleteFlight method allows the view to enable/disable the Delete button.
     * The ConfirmDeleteFlightRequested event allows the view to display a confirmation before deleting a flight.
     * 
     * ----- XAML -----
     * (Open BellaCodeAir/Styles/Main.xaml and review the Main.View style)
     * The DataContext for this style null.
     * The view creates an MainViewModel using the ViewModelScope element.
     * The view binds the Reset, Add, and DeleteFlights commands to methods in the view model.
     *   For Reset, the BindCommandBehavior uses convention to pair the command and method that have the same name.
     *   For Add and Delete, the BindCommandBehavior is provided the method names because they differs from the command.
     * The view uses an event trigger to call ConfirmDeleteFlightAction whenever the ConfirmDeleteFlightRequested event is raised.
     * The view passes the Flights property as the DataContext to the Flights.View and the Main.DateTimeSlider views.
     * 
     * (Open BellaCodeAir/Styles/Flights.xaml and review the Flights.ArrivingFlight style)
     * This view binds the Delete command to a button.  
     *   When the user clicks this button the command bubbles up the XAML hierarchy and is handled by the MainViewModel.          
    */

    public class MainViewModel : ViewModel
    {
        private IFlightData _flightData;

        public MainViewModel(IFlightData flightData)
        {
            if (flightData == null)
            {
                throw new ArgumentNullException("flightData");
            }

            this._flightData = flightData;

            this.Flights = new ObservableCollection<Flight>();
            InitializeFlights();
        }

        private void InitializeFlights()
        {
            var now = DateTime.UtcNow;
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                this.AddFlight();
            }
        }

        public ObservableCollection<Flight> Flights {get; private set;}

        public void Reset()
        {
            this.Flights.Clear();
        }

        public void AddFlight()
        {
            var flight = this._flightData.CreateFlight();
            this.Flights.Add(flight);
        }        

        public bool CanDeleteFlight(Flight flight)
        {
            return flight != null;
        }

        public void DeleteFlight(Flight flight)
        {
            if (RaiseConfirmDeleteFlightRequested(flight))
            {
                this.Flights.Remove(flight);
            }
        }

        public event EventHandler<InteractionEventArgs<Flight, bool>> ConfirmDeleteFlightRequested;
        
        private bool RaiseConfirmDeleteFlightRequested(Flight flight)
        {
            if (this.ConfirmDeleteFlightRequested != null)
            {
                var e = new InteractionEventArgs<Flight, bool>(flight);
                this.ConfirmDeleteFlightRequested(this, e);
                return e.Result;
            }

            return false;
        }        
    }
}
