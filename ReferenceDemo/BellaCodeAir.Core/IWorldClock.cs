namespace BellaCodeAir
{
    using System;
    using System.ComponentModel;

    public interface IWorldClock : INotifyPropertyChanged
    {
        DateTime CurrentDateTime {get; set;}
    }
}
