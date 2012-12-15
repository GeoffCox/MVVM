namespace BellaCodeAir
{
    using System.Collections.Generic;
    using BellaCodeAir.Models;

    // This class is built for demonstration purposes only.
    // A real application would likely use the repository pattern.
    public interface IFlightData
    {
        IEnumerable<Airline> Airlines { get; }

        IEnumerable<Airport> Airports { get; }

        Airport HomeAirport { get; }

        Flight CreateFlight();        
    }
}
