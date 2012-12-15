namespace BellaCodeAir.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Threading;
    using BellaCode.Mvvm;
    using BellaCodeAir.Models;

    // TODO: #06 - A view model doing asynchronous work
    // --------------------------------------------------------------------------------
    /*
     * WorldClockViewModel uses the collection of Flight instances as its model to bound the date/time range of the world clock.
     * The Percentage and CurrentDateTime properties are kept synchronized.
     * The Start and Pause methods allow the view to control the timer.
     * The CanStart and CanPause propeties allow the view to enable/disable the buttons.
     * The timer is used to regularly update the slider percentage.
     * The _updatingClock and _updatingPercentage fields are used to prevent infinite looping.
     * 
     * The world clock is used by the FlightViewModel to determine the Status of the flight.
     * 
     * ----- XAML -----
     * (Open BellaCodeAir/Styles/Main.xaml and review the Main.DateTimeSlider style)
     * The DataContext for this is the ObservableCollection<Flight>.
     * The view creates an WorldClockViewModel using the ViewModelScope element.
     * The view binds the Play and Pause commands to methods in the view model.
     *   The BindCommandBehavior uses convention to pair the command and method that have the same name.
     * The view binds to the CurrentDateTime and Percentage properties in in the view model.
    */

    public class WorldClockViewModel : ViewModel<ObservableCollection<Flight>>
    {
        private IWorldClock _worldClock;

        public WorldClockViewModel(IWorldClock worldClock)
        {
            if (worldClock == null)
            {
                throw new ArgumentNullException("worldClock");
            }

            this._worldClock = worldClock;
            this._worldClock.PropertyChanged += new PropertyChangedEventHandler(WorldClock_PropertyChanged);

            this._timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this._timer.Tick += new EventHandler(Timer_Tick);
        }

        private ObservableCollection<Flight> Flights
        {
            get
            {
                return this.Model;
            }
        }

        private double _percentage;

        public double Percentage
        {
            get
            {
                return this._percentage;
            }
            set
            {
                if (this._percentage != value)
                {
                    this._percentage = value;
                    this.RaisePropertyChanged("Percentage");

                    this.UpdateClock();
                }
            }
        }

        public DateTime CurrentDateTime
        {
            get
            {
                return this._worldClock.CurrentDateTime;
            }
            private set
            {
                this._worldClock.CurrentDateTime = value;
            }
        }

        private bool _updatingClock = false;

        private void UpdateClock()
        {
            if (!this._updatingPercentage)
            {
                this._updatingClock = true;

                TimeSpan delta = this._maximumDateTime - this._minimumDateTime;
                if (delta > TimeSpan.Zero)
                {
                    this.CurrentDateTime = this._minimumDateTime + TimeSpan.FromMilliseconds(delta.TotalMilliseconds * (this._percentage));
                }

                this._updatingClock = false;
            }
        }

        private bool _updatingPercentage = false;

        private void UpdatePercentage()
        {
            if (!this._updatingClock)
            {
                this._updatingPercentage = true;

                double newPercentage = 0;
                TimeSpan delta = this._maximumDateTime - this._minimumDateTime;
                if (delta > TimeSpan.Zero)
                {
                    newPercentage = ((this._worldClock.CurrentDateTime - this._minimumDateTime).TotalMilliseconds / delta.TotalMilliseconds);
                }

                this.Percentage = Math.Max(Math.Min(newPercentage, 1.0), 0.0);
                this._updatingPercentage = false;
            }
        }

        private DateTime _minimumDateTime = DateTime.MinValue;
        private DateTime _maximumDateTime = DateTime.MaxValue;

        private void UpdateClockMinMax()
        {
            DateTime minDateTime = DateTime.MaxValue;
            DateTime maxDateTime = DateTime.MinValue;

            if (this.Flights != null && this.Flights.FirstOrDefault() != null)
            {
                foreach (var flight in this.Flights)
                {
                    if (flight.DepartureDateTime < minDateTime)
                    {
                        minDateTime = flight.DepartureDateTime;
                    }
                    if (flight.ArrivalDateTime < minDateTime)
                    {
                        minDateTime = flight.ArrivalDateTime;
                    }

                    if (flight.DepartureDateTime > maxDateTime)
                    {
                        maxDateTime = flight.DepartureDateTime;
                    }
                    if (flight.ArrivalDateTime > maxDateTime)
                    {
                        maxDateTime = flight.ArrivalDateTime;
                    }
                }

                this._minimumDateTime = minDateTime.AddHours(-1);
                this._maximumDateTime = maxDateTime.AddHours(1);
            }
            else
            {
                this._minimumDateTime = DateTime.Now;
                this._maximumDateTime = this._minimumDateTime;
            }

            this.EnsureCurrentDateTimeInRange();
            this.UpdatePercentage();
        }

        private void EnsureCurrentDateTimeInRange()
        {
            var newValue = this.CurrentDateTime >= this._minimumDateTime ? this.CurrentDateTime : this._minimumDateTime;
            newValue = this.CurrentDateTime <= this._maximumDateTime ? this.CurrentDateTime : this._maximumDateTime;
            this.CurrentDateTime = newValue;
        }

        private DispatcherTimer _timer = new DispatcherTimer();

        public bool CanPlay
        {
            get
            {
                return !this._timer.IsEnabled;
            }
        }

        public void Play()
        {
            if (this.Percentage >= 1.0)
            {
                this.Percentage = 0.0;
            }

            this._timer.Start();
        }

        public bool CanPause
        {
            get
            {
                return this._timer.IsEnabled;
            }
        }

        public void Pause()
        {
            this._timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.Percentage >= 1.0)
            {
                this._timer.Stop();
                this.RaisePropertyChanged("CanPlay");
                this.RaisePropertyChanged("CanPause");
            }

            this.Percentage += 0.01;
        }

        protected override void OnModelChanged(ObservableCollection<Flight> oldValue, ObservableCollection<Flight> newValue)
        {
            base.OnModelChanged(oldValue, newValue);

            if (oldValue != null)
            {
                oldValue.CollectionChanged -= this.Flights_CollectionChanged;
            }
            if (newValue != null)
            {
                newValue.CollectionChanged += this.Flights_CollectionChanged;
            }

            this.UpdateClockMinMax();
        }

        private void Flights_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateClockMinMax();
        }

        private void WorldClock_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentDateTime":
                    this.EnsureCurrentDateTimeInRange();
                    this.UpdatePercentage();
                    this.RaisePropertyChanged("CurrentDateTime");
                    break;
            }
        }
    }
}
