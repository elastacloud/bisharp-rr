using BISharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISharp.Contracts;
using rr_dashboard.Classes;
using System.Threading;
using System.IO;

namespace rr_dashboard
{
    class Program
    {
        const string PowerByTheHour = "PowerByTheHour";
       /* static void Main(string[] args)
        {
            var pbi = new PowerBiAuthentication("007fb3ab-bf10-437b-9bea-1825f1086d00");
            var dsClient = new DatasetsClient(pbi);

            var datasets = dsClient.List().Result;
            string powerByTheHourDatasetId = string.Empty;
            if (!datasets.value.Any(t => t.name == PowerByTheHour))
            {
                var result = dsClient.Create(PowerByTheHour, true, typeof(Classes.FlightInfo)).Result;
                powerByTheHourDatasetId = result.id;
            }
            else
            {
                powerByTheHourDatasetId = datasets.value.First(t => t.name == PowerByTheHour).id;
            }

            while (true)
            {
                foreach (var item in Directory.GetFiles(@"C:\tmp\fd\"))
                {
                    dsClient.ClearRows(powerByTheHourDatasetId, typeof(FlightInfo).Name).Wait();
                    var data = File.ReadAllText(item);
                    var flights = JsonConvert.DeserializeObject<CurrentFlights>(data);
                    dsClient.AddRows(powerByTheHourDatasetId, typeof(FlightInfo).Name, GetTableRows(flights))
                        .Wait();

                    Thread.Sleep(15000);
                }
            }
        }*/

       /* private static TableRows<FlightInfo> GetTableRows(CurrentFlights flights)
        {
            return new TableRows<FlightInfo>()
            {
                rows = flights.mrkrs.Select(
                mrkr => new FlightInfo
                {
                    Altitude = mrkr.inf.al,
                    Manufacturer = mrkr.inf.br,
                    Company = mrkr.inf.co,
                    Callsign = mrkr.inf.cs,
                    ReadingTime = mrkr.inf.dt,
                    GroundSpeed = mrkr.inf.gs,
                    ICAO = mrkr.inf.ia,
                    Model = mrkr.inf.mo,
                    rc = mrkr.inf.rc,
                    Squawk = mrkr.inf.sq,
                    Heading = mrkr.inf.tr,
                    vs = mrkr.inf.vs,
                    Latitude = mrkr.pt[0],
                    Longitude = mrkr.pt[1]
                }
                ).ToList()
            };
        }*/
    }
}
