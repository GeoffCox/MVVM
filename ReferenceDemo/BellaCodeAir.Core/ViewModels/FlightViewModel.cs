namespace BellaCodeAir.ViewModels
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using BellaCode.Mvvm;
    using BellaCodeAir.Models;
    using System;

    // TODO: #02 - A view model for a model
    // --------------------------------------------------------------------------------
    /*
     * FlightViewModel is a ViewModel that takes a Flight instance as its model.  
     * The view is allowed to bind directly to the model. FlightViewModel exposes a property named 'Flight' to provide a more meaningful name than 'Model' for binding.
     * The Status property demonstrates combining model properties and external data to calculate a new value for the view.
     * The OnModelChanged and OnViewChanged methods help ensure the view models properties are up to date.    
     * 
     * ----- XAML -----
     * (Open BellaCodeAir/Styles/Flights.xaml and review the Flights.ArrivingFlight style)
     * The view binds several to several properties on the model.
     * The view provides a delete button with an associated command. The command execution is handled in the MainViewModel.
     * 
     * (Open BellaCodeAir/Styles/Indicators.xaml and review the Flights.Status style)
     * The view binds to the Status property on the view model to display an icon based on the status value.
     * The view uses a value converter to display a tooltip that describes the status.
     * The view uses data triggers to animate the transitions between status values.     
    */

    public sealed class FlightViewModel : ViewModel<Flight>
    {
        private IWorldClock _worldClock;

        public FlightViewModel(IWorldClock worldClock)
        {
            if (worldClock == null)
            {
                throw new ArgumentNullException("worldClock");
            }

            this._worldClock = worldClock;
            this._worldClock.PropertyChanged += new PropertyChangedEventHandler(WorldClock_PropertyChanged);
        }
        
        public Flight Flight
        {
            get
            {
                return this.Model;
            }
        }
       
        private FlightStatus _status;

        public FlightStatus Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status != value)
                {
                    this._status = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }

        private void UpdateStatus()
        {
            this.Status = FlightStatus.Unknown;

            if (this.Flight != null)
            {
                if (this.Flight.ArrivalDateTime < this._worldClock.CurrentDateTime)
                {
                    this.Status = FlightStatus.Arrived;
                }
                else if (this.Flight.DepartureDateTime < this._worldClock.CurrentDateTime)
                {
                    this.Status = FlightStatus.InFlight;
                }
                else
                {
                    this.Status = FlightStatus.WaitingForDeparture;
                }
            }            
        }

        protected override void OnModelChanged(Flight oldValue, Flight newValue)
        {
            base.OnModelChanged(oldValue, newValue);

            this.UpdateStatus();
        }

        protected override void OnViewChanged(object oldValue, object newValue)
        {
            base.OnViewChanged(oldValue, newValue);

            // This calls update status on the foreground thread asynchronously.
            // This handles the special case of a flight being added when the clock indicates that the flight is already underway.
            var foregroundTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() => { })
                .ContinueWith((t) =>
                {
                    this.UpdateStatus();
                }, foregroundTaskScheduler);
        }

        private void WorldClock_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.UpdateStatus();
        }
    }
}
