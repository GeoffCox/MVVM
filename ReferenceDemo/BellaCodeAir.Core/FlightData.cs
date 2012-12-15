namespace BellaCodeAir
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BellaCodeAir.Models;

    // This class is built for demonstration purposes only.
    // A real application would likely use the repository pattern.
    public class FlightData : IFlightData
    {
        #region Airline Data

        private static Airline[] _airlines = new Airline[]
        {
            new Airline("A","Alaska"),
            new Airline("D","Delta"),
            new Airline("F","Frontier"),
            new Airline("U","United"),
        };

        #endregion

        #region Airport Data

        private static Airport[] _Airports = new Airport[]
        {
            new Airport("ANC","Anchorage International Airport"),
            new Airport("BHM","Birmingham International Airport"),
            new Airport("CHO","Charlottesville-Albemarle Airport"),
            new Airport("DAL","Dallas Love Field"),
            new Airport("EFD","Ellington Field"),
            new Airport("FSM","Fort Smith Airport"),
            new Airport("GCK","Garden City Regional Airport"),
            new Airport("HKS","Hawkins Field Airport"),
            new Airport("IAD","Washington Dulles International Airport"),
            new Airport("JFK","John F. Kennedy International Airport"),
            new Airport("KOA","Kona International Airport"),
            new Airport("LAX","Los Angeles International Airport"),
            new Airport("MCI","Kansas City International Airport"),
            new Airport("NEW","Lakefront Airport"),
            new Airport("ORD","Chicago O'hare International Airport"),
            new Airport("PDX","Portland International Airport"),
            new Airport("RDU","Raleigh-Durham International Airport"),
            new Airport("SEA","Seattle/Tacoma International Airport"),
            new Airport("TEX","Telluride Regional Airport"),
            new Airport("UUU","Newport State Airport"),
            new Airport("VGT","North Las Vegas Air Terminal"),
            new Airport("WYS","West Yellowstone Airport"),
            new Airport("X44","Miami Airport"),
            new Airport("YKM","Yakima Air Terminal/Mcallister Field"),
            new Airport("Z08","Ofu Airport"),            
        };

        #endregion

        public FlightData()
        {
            this.HomeAirport = this.Airports.First(x => x.Code == _homeAirportCode);
        }

        public IEnumerable<Airline> Airlines
        {
            get { return _airlines; }
        }

        public IEnumerable<Airport> Airports
        {
            get { return _Airports; }
        }

        private static string _homeAirportCode = "SEA";

        public Airport HomeAirport {get; private set;}

        private Random _random = new Random();
        private HashSet<string> _flightKeys = new HashSet<string>();

        public Flight CreateFlight()
        {           
            var flight = new Flight();

            SetFlightCodeAndNumber(flight);
            SetFlightOriginAndDestination(flight);
            SetFlightDepartureAndArrivalDateTimes(flight);

            return flight;
        }

        public void Reset()
        {
            this._flightKeys.Clear();
        }

        private void SetFlightCodeAndNumber(Flight flight)
        {
            var airlineList = this.Airlines.ToList();

            string flightKey = null;
            Airline airline = null;
            int number = 0;

            while (flightKey == null || _flightKeys.Contains(flightKey))
            {
                airline = airlineList[this._random.Next(airlineList.Count)];
                number = this._random.Next(9999);
                flightKey = airline.Code + "/" + number.ToString();
            }

            _flightKeys.Add(flightKey);

            flight.Airline = airline;
            flight.Number = number;            
        }

        private void SetFlightOriginAndDestination(Flight flight)
        {
            var airportList = this.Airports.Where(x => x != this.HomeAirport).ToList();
            var airport = airportList[this._random.Next(airportList.Count)];

            bool isDepartingFlight = this._random.Next(0, 2) > 0;

            if (isDepartingFlight)
            {
                flight.Origin = this.HomeAirport;
                flight.Destination = airport;
            }
            else
            {
                flight.Origin = airport;
                flight.Destination = this.HomeAirport;
            }
        }

        private void SetFlightDepartureAndArrivalDateTimes(Flight flight)
        {
            flight.DepartureDateTime = DateTime.Now.AddHours(this._random.Next(8)).AddMinutes(this._random.Next(60));
            flight.ArrivalDateTime = flight.DepartureDateTime.AddHours(this._random.Next(1, 8)).AddMinutes(this._random.Next(60));
        }
    }
}
