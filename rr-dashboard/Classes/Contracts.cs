using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_dashboard.Classes
{
    public interface ITableEntry
    {
        
    }


    public class FlattenedFlightInfoTableEntry : ITableEntry
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }

        public string Callsign { get; set; }

        public int Squawk { get; set; }

        public string ICAO { get; set; }

        public string Company { get; set; }

        public string RegistrationCode { get; set; }

        public int Heading { get; set; }
        public int Altitude { get; set; }

        public int GroundSpeed { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public object ReadingTime { get; set; }

        public int VerticalSpeed { get; set; }

        public String Type { get; set; }
        public double Reading { get; set; }
        public bool Predicted { get; set; }
    }

    public class FlightInfoTableEntry : ITableEntry
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }

        public string Callsign { get; set; }

        public int Squawk { get; set; }

        public string ICAO { get; set; }

        public string Company { get; set; }

        public string RegistrationCode { get; set; }
    }

    public class PositionTableEntry : ITableEntry
    {
        public string ICAO { get; set; } //key
        public int Heading { get; set; }
        public int Altitude { get; set; }

        public int GroundSpeed { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public object ReadingTime { get; set; }

        public int VerticalSpeed { get; set; }
    }

    public class SensorTableEntry : ITableEntry
    {
        public string ICAO { get; set; } // key
        public object ReadingTime { get; set; } //ey
        public String Type { get; set; }
        public double Reading { get; set; }
        public bool Predicted { get; set; }
    }

    public class FlightInfo
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }

        public string Callsign { get; set; }

        public int Squawk { get; set; }

        public string ICAO { get; set; }

        public string Company { get; set; }

        public string RegistrationCode { get; set; }

        public List<Position> Positions { get; set; }

    }

    public class Position
    {
        public int Heading { get; set; }
        public int Altitude { get; set; }

        public int GroundSpeed { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public object ReadingTime { get; set; }

        public int VerticalSpeed { get; set; }

        public Sensors Sensors { get; set; }
    }

    public class Sensors
    {
        public Sensor Sensor1 { get; set; }
        public Sensor Sensor2 { get; set; }
        public Sensor Sensor3 { get; set; }
        public Sensor Sensor4 { get; set; }
        public Sensor Sensor5 { get; set; }
        public Sensor Sensor6 { get; set; }
    }

    public class Sensor
    {
        public String Type { get; set; }
        public double Reading { get; set; }
        public bool Predicted { get; set; }
    }

    public class FlightInfo2
    {
        public string co { get; set; }
        public int sq { get; set; }
        public int tr { get; set; }
        public int al { get; set; }
        public string cs { get; set; }
        public int vs { get; set; }
        public string ia { get; set; }
        public int gs { get; set; }
        public object dt { get; set; }
        public string mo { get; set; }
        public string br { get; set; }
        public string rc { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class Inf
    {
        public string co { get; set; }
        public int sq { get; set; }
        public int tr { get; set; }
        public int al { get; set; }
        public string cs { get; set; }
        public int vs { get; set; }
        public string ia { get; set; }
        public int gs { get; set; }
        public object dt { get; set; }
        public string mo { get; set; }
        public string br { get; set; }
        public string rc { get; set; }
    }

    public class Mkr
    {
        public string id { get; set; }
        public List<double> pt { get; set; }
        public int an { get; set; }
        public Inf inf { get; set; }
        public List<object> ln { get; set; }
    }

    public class Arpts
    {
    }

    public class Prv
    {
        public List<double> pt { get; set; }
        public string st { get; set; }
    }

    public class CurrentFlights
    {
        public List<Mkr> mrkrs { get; set; }
        public int num { get; set; }
        public Arpts arpts { get; set; }
        public List<Prv> prv { get; set; }
    }

}
