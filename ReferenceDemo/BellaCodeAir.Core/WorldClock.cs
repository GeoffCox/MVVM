namespace BellaCodeAir
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A singleton that provides the date time for all flights in the world.
    /// </summary>
    public class WorldClock : IWorldClock
    {
        private DateTime _currentDateTime = DateTime.Now;

        public DateTime CurrentDateTime
        {
            get
            {
                return this._currentDateTime;
            }
            set
            {
                if (this._currentDateTime != value)
                {
                    this._currentDateTime = value;
                    this.RaisePropertyChanged("Current");
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
