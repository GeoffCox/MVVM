namespace BellaCodeAir.Models
{
    using System;
    using System.ComponentModel;

    // TODO: #01 - A model
    // --------------------------------------------------------------------------------
    /*
     * Flight is a typical model class.  
     * It implements INotifyPropertyChanged.
     * It has no knowledge of the user interface or application logic.
     *      
     * In .NET 4.5, the CallerMemberName attribute can make raising property changes not require the string name of the property be hard-coded.
     * You can consider deriving all model classes from a base class that implements INotifyPropertyChanged.
    */

    public sealed class Flight : INotifyPropertyChanged
    {
        public Flight()
        {
            this._id = Guid.NewGuid();
        }

        private Guid _id;

        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                if (this._id != value)
                {
                    this._id = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }

        private Airline _airline;

        public Airline Airline
        {
            get
            {
                return this._airline;
            }
            set
            {
                if (this._airline != value)
                {
                    this._airline = value;
                    this.RaisePropertyChanged("Airline");
                }
            }
        }

        private int _number;

        public int Number
        {
            get
            {
                return this._number;
            }
            set
            {
                if (this._number != value)
                {
                    this._number = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }

        private Airport _origin;

        public Airport Origin
        {
            get
            {
                return this._origin;
            }
            set
            {
                if (this._origin != value)
                {
                    this._origin = value;
                    this.RaisePropertyChanged("Origin");
                }
            }
        }

        private Airport _destination;

        public Airport Destination
        {
            get
            {
                return this._destination;
            }
            set
            {
                if (this._destination != value)
                {
                    this._destination = value;
                    this.RaisePropertyChanged("Destination");
                }
            }
        }

        private DateTime _departureDateTime;

        public DateTime DepartureDateTime
        {
            get
            {
                return this._departureDateTime;
            }
            set
            {
                if (this._departureDateTime != value)
                {
                    this._departureDateTime = value;
                    this.RaisePropertyChanged("DepartureDateTime");
                }
            }
        }      

        private DateTime _arrivalDateTime;

        public DateTime ArrivalDateTime
        {
            get
            {
                return this._arrivalDateTime;
            }
            set
            {
                if (this._arrivalDateTime != value)
                {
                    this._arrivalDateTime = value;
                    this.RaisePropertyChanged("ArrivalDateTime");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
