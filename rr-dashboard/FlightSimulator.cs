using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISharp;
using BISharp.Contracts;
using Newtonsoft.Json;
using rr_dashboard.Classes;

namespace rr_dashboard
{
    class FlightSimulator
    {
        const string PowerByTheHour = "PowerByTheHour";
        private static Dictionary<String, FlightInfo> flightDictionary = new Dictionary<String, FlightInfo>();

        private static TableRows<FlightInfoTableEntry> flightTable = new TableRows<FlightInfoTableEntry>(); 
        private static TableRows<PositionTableEntry> positionTable = new TableRows<PositionTableEntry>();
        private static TableRows<SensorTableEntry> sensorTable = new TableRows<SensorTableEntry>();

        private static TableRows<FlattenedFlightInfoTableEntry> flattenedFlightDictionary = new TableRows<FlattenedFlightInfoTableEntry>();

        private static int Buffer = 10000;
        private static string DataSource = @"C:\tmp\fd\";

        private async static void test(GroupClient groupClient)
        {
            var res = await groupClient.Get();
            Console.WriteLine();
        }
        public static void Main(string[] args)
        {


            ReadInData(DataSource);
            FlattenPopulateTables();
        
            
            var pbi = new PowerBiAuthentication("007fb3ab-bf10-437b-9bea-1825f1086d00");
            var dsClient = new DatasetsClient(pbi);
            var groupClient = new GroupClient(pbi);

            var group = groupClient.Get().Result.value.First(t=>t.name== "ElastacoudPowerByTheHour");

            //BufferedInsert<FlightInfoTableEntry>(dsClient, powerByTheHourDatasetId, flightTable);
            //BufferedInsert<PositionTableEntry>(dsClient, powerByTheHourDatasetId, positionTable);
            //BufferedInsert<SensorTableEntry>(dsClient, powerByTheHourDatasetId, sensorTable);
            try
            {
                string powerByTheHourDatasetId = CreateBIDataset(dsClient, group);
                BufferedInsert<FlattenedFlightInfoTableEntry>(dsClient, group, powerByTheHourDatasetId,
                    flattenedFlightDictionary);
            }
            catch (Exception e)
            {
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static string CreateBIDataset(DatasetsClient client, Group g)
        {
            var datasets = client.List(g.id).Result;
            
            if (datasets.value.All(t => t.name != PowerByTheHour))
            {
                var result = client.Create(g.id, PowerByTheHour, false, /*typeof(Classes.FlightInfoTableEntry), typeof(Classes.PositionTableEntry), typeof(Classes.SensorTableEntry),*/ typeof(Classes.FlattenedFlightInfoTableEntry)).Result;
                return result.id;
            }
            else
            {
               return datasets.value.First(t => t.name == PowerByTheHour).id;
            }
        }

        private static void ReadInData(String pathToData)
        {
            foreach (var item in Directory.GetFiles(pathToData))
            {

                var data = File.ReadAllText(item);
                try
                {
                    var flights = JsonConvert.DeserializeObject<CurrentFlights>(data);
                    PopulateDictionary(flights);


                }
                catch (Exception w)
                {

                    Console.WriteLine(item);
                }


                //Thread.Sleep(15000);
            }
        }

        private static void BufferedInsert<T>(DatasetsClient client, Group g, String datasetId, TableRows<T> entries)
        {
 
            var length = entries.rows.Count;
            var numOfSkips = (int)Math.Ceiling((double)length / Buffer);

            var count = 0;
            while (count <= numOfSkips)
            {
                TableRows<T> bufferTableEntries = new TableRows<T>();
                var numremaining = length - count * Buffer;
                bufferTableEntries.rows.AddRange(entries.rows.Skip(count).Take(numremaining > Buffer ? Buffer : numremaining));
                client.AddRows(g.id, datasetId, typeof(T).Name, bufferTableEntries).Wait();
                count++;
            }
        }

        private static void FlattenPopulateTables()
        {
            foreach (var flight in flightDictionary.Values)
            {
                foreach (var position in flight.Positions)
                {

                    Sensors sensors = position.Sensors;

                    flattenedFlightDictionary.rows.AddRange(new List<FlattenedFlightInfoTableEntry>()
                    {
                        new FlattenedFlightInfoTableEntry()
                        {
                            ICAO = flight.ICAO,
                            Callsign = flight.Callsign,
                            RegistrationCode = flight.RegistrationCode,
                            Company = flight.Company,
                            Manufacturer = flight.Manufacturer,
                            Model = flight.Model,
                            Squawk = flight.Squawk,
                            VerticalSpeed = position.VerticalSpeed,
                            GroundSpeed = position.VerticalSpeed,
                            Altitude = position.Altitude,
                            Heading = position.Heading,
                            Latitude = position.Altitude,
                            Longitude = position.Longitude,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor1.Type,
                            Reading = sensors.Sensor1.Reading,
                            Predicted = sensors.Sensor1.Predicted

                        },
                        new FlattenedFlightInfoTableEntry()
                        {
                            ICAO = flight.ICAO,
                            Callsign = flight.Callsign,
                            RegistrationCode = flight.RegistrationCode,
                            Company = flight.Company,
                            Manufacturer = flight.Manufacturer,
                            Model = flight.Model,
                            Squawk = flight.Squawk,
                            VerticalSpeed = position.VerticalSpeed,
                            GroundSpeed = position.VerticalSpeed,
                            Altitude = position.Altitude,
                            Heading = position.Heading,
                            Latitude = position.Altitude,
                            Longitude = position.Longitude,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor2.Type,
                            Reading = sensors.Sensor2.Reading,
                            Predicted = sensors.Sensor2.Predicted
                        },
                        new FlattenedFlightInfoTableEntry()
                        {
                            ICAO = flight.ICAO,
                            Callsign = flight.Callsign,
                            RegistrationCode = flight.RegistrationCode,
                            Company = flight.Company,
                            Manufacturer = flight.Manufacturer,
                            Model = flight.Model,
                            Squawk = flight.Squawk,
                            VerticalSpeed = position.VerticalSpeed,
                            GroundSpeed = position.VerticalSpeed,
                            Altitude = position.Altitude,
                            Heading = position.Heading,
                            Latitude = position.Altitude,
                            Longitude = position.Longitude,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor3.Type,
                            Reading = sensors.Sensor3.Reading,
                            Predicted = sensors.Sensor3.Predicted
                        },
                        new FlattenedFlightInfoTableEntry()
                        {
                            ICAO = flight.ICAO,
                            Callsign = flight.Callsign,
                            RegistrationCode = flight.RegistrationCode,
                            Company = flight.Company,
                            Manufacturer = flight.Manufacturer,
                            Model = flight.Model,
                            Squawk = flight.Squawk,
                            VerticalSpeed = position.VerticalSpeed,
                            GroundSpeed = position.VerticalSpeed,
                            Altitude = position.Altitude,
                            Heading = position.Heading,
                            Latitude = position.Altitude,
                            Longitude = position.Longitude,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor4.Type,
                            Reading = sensors.Sensor4.Reading,
                            Predicted = sensors.Sensor4.Predicted
                        },
                        new FlattenedFlightInfoTableEntry()
                        {
                            ICAO = flight.ICAO,
                            Callsign = flight.Callsign,
                            RegistrationCode = flight.RegistrationCode,
                            Company = flight.Company,
                            Manufacturer = flight.Manufacturer,
                            Model = flight.Model,
                            Squawk = flight.Squawk,
                            VerticalSpeed = position.VerticalSpeed,
                            GroundSpeed = position.VerticalSpeed,
                            Altitude = position.Altitude,
                            Heading = position.Heading,
                            Latitude = position.Altitude,
                            Longitude = position.Longitude,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor5.Type,
                            Reading = sensors.Sensor5.Reading,
                            Predicted = sensors.Sensor5.Predicted
                        },
                        new FlattenedFlightInfoTableEntry()
                        {
                            ICAO = flight.ICAO,
                            Callsign = flight.Callsign,
                            RegistrationCode = flight.RegistrationCode,
                            Company = flight.Company,
                            Manufacturer = flight.Manufacturer,
                            Model = flight.Model,
                            Squawk = flight.Squawk,
                            VerticalSpeed = position.VerticalSpeed,
                            GroundSpeed = position.VerticalSpeed,
                            Altitude = position.Altitude,
                            Heading = position.Heading,
                            Latitude = position.Altitude,
                            Longitude = position.Longitude,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor6.Type,
                            Reading = sensors.Sensor6.Reading,
                            Predicted = sensors.Sensor6.Predicted
                        }
                    });
                }
            }
        }

        private static void PopulateTables()
        {
            foreach (var flight in flightDictionary.Values)
            {
                flightTable.rows.Add(new FlightInfoTableEntry()
                {
                    ICAO = flight.ICAO,
                    Callsign = flight.Callsign,
                    RegistrationCode = flight.RegistrationCode,
                    Company = flight.Company,
                    Manufacturer = flight.Manufacturer,
                    Model = flight.Model,
                    Squawk = flight.Squawk
                });

                foreach (var position in flight.Positions)
                {
                    positionTable.rows.Add(new PositionTableEntry()
                    {
                        ICAO = flight.ICAO,
                        VerticalSpeed = position.VerticalSpeed,
                        GroundSpeed = position.VerticalSpeed,
                        Altitude = position.Altitude,
                        Heading = position.Heading,
                        Latitude = position.Altitude,
                        Longitude = position.Longitude,
                        ReadingTime = position.ReadingTime

                    });

                    Sensors sensors = position.Sensors;

                    sensorTable.rows.AddRange(new List<SensorTableEntry>()
                    {
                        new SensorTableEntry()
                        {
                            ICAO = flight.ICAO,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor1.Type,
                            Reading = sensors.Sensor1.Reading,
                            Predicted = sensors.Sensor1.Predicted

                        },
                        new SensorTableEntry()
                        {
                            ICAO = flight.ICAO,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor2.Type,
                            Reading = sensors.Sensor2.Reading,
                            Predicted = sensors.Sensor2.Predicted
                        },
                        new SensorTableEntry()
                        {
                            ICAO = flight.ICAO,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor3.Type,
                            Reading = sensors.Sensor3.Reading,
                            Predicted = sensors.Sensor3.Predicted
                        },
                        new SensorTableEntry()
                        {
                            ICAO = flight.ICAO,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor4.Type,
                            Reading = sensors.Sensor4.Reading,
                            Predicted = sensors.Sensor4.Predicted
                        },
                        new SensorTableEntry()
                        {
                            ICAO = flight.ICAO,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor5.Type,
                            Reading = sensors.Sensor5.Reading,
                            Predicted = sensors.Sensor5.Predicted
                        },
                        new SensorTableEntry()
                        {
                            ICAO = flight.ICAO,
                            ReadingTime = position.ReadingTime,
                            Type = sensors.Sensor6.Type,
                            Reading = sensors.Sensor6.Reading,
                            Predicted = sensors.Sensor6.Predicted
                        }
                    });
                }
            }
        }

        private static void PopulateDictionary(CurrentFlights flights)
        {
            Random rand = new Random();
            flights.mrkrs.ForEach(mrkr =>
            {
                var flightInfo = new FlightInfo()
                {
                    Manufacturer = mrkr.inf.br,
                    Company = mrkr.inf.co,
                    Callsign = mrkr.inf.cs,
                    ICAO = mrkr.inf.ia,
                    Model = mrkr.inf.mo,
                    Squawk = mrkr.inf.sq,
                    RegistrationCode = mrkr.inf.rc,
                    Positions = new List<Position>()
                    {
                        new Position()
                        {
                            Altitude = mrkr.inf.al,
                            ReadingTime = mrkr.inf.dt,
                            GroundSpeed = mrkr.inf.gs,
                            Heading = mrkr.inf.tr,
                            VerticalSpeed = mrkr.inf.vs,
                            Latitude = mrkr.pt[0],
                            Longitude = mrkr.pt[1],
                            Sensors = new Sensors()
                            {
                                Sensor1 = new Sensor()
                                {
                                    Type = "Type1",
                                    Reading =
                                        rand.Next(1000) == 1
                                            ? 100 + rand.Next(-1, 1)*((float) rand.Next(1, 15)/100)
                                            : 100
                                },
                                Sensor2 = new Sensor()
                                {
                                    Type = "Type2",
                                    Reading =
                                        rand.Next(1000) == 1
                                            ? 200 + rand.Next(-1, 1)*((float) rand.Next(5, 25)/100)
                                            : 200
                                },
                                Sensor3 = new Sensor()
                                {
                                    Type = "Type3",
                                    Reading =
                                        rand.Next(5000) == 1 ? 50 + rand.Next(-1, 1)*((float) rand.Next(1, 10)/100) : 50
                                },
                                Sensor4 = new Sensor()
                                {
                                    Type = "Type4",
                                    Reading =
                                        rand.Next(1000) == 1
                                            ? 68 + rand.Next(-1, 1)*((float) rand.Next(10, 50)/100)
                                            : 68
                                },
                                Sensor5 = new Sensor()
                                {
                                    Type = "Type5",
                                    Reading =
                                        rand.Next(1000) == 1
                                            ? 5000 + rand.Next(-1, 1)*((float) rand.Next(10, 50)/100)
                                            : 5000
                                },
                                Sensor6 = new Sensor()
                                {
                                    Type = "Type6",
                                    Reading =
                                        rand.Next(1000) == 1 ? 1 + rand.Next(-1, 1)*((float) rand.Next(10, 15)/100) : 1
                                }
                            }
                        }
                    }

                };


                if (flightInfo.Positions[0].Sensors.Sensor1.Reading.Equals(100.0) != true)
                    flightInfo.Positions[0].Sensors.Sensor1.Predicted = true;
                if (flightInfo.Positions[0].Sensors.Sensor2.Reading.Equals(200.0) != true)
                    flightInfo.Positions[0].Sensors.Sensor2.Predicted = true;
                if (flightInfo.Positions[0].Sensors.Sensor3.Reading.Equals(50.0) != true)
                    flightInfo.Positions[0].Sensors.Sensor3.Predicted = true;
                if (flightInfo.Positions[0].Sensors.Sensor4.Reading.Equals(68.0) != true)
                    flightInfo.Positions[0].Sensors.Sensor4.Predicted = true;
                if (flightInfo.Positions[0].Sensors.Sensor5.Reading.Equals(5000.0) != true)
                    flightInfo.Positions[0].Sensors.Sensor5.Predicted = true;
                if (flightInfo.Positions[0].Sensors.Sensor6.Reading.Equals(1.0) != true)
                    flightInfo.Positions[0].Sensors.Sensor6.Predicted = true;


                if (flightDictionary.ContainsKey(flightInfo.ICAO))
                {
                    flightDictionary[flightInfo.ICAO].Positions.AddRange(flightInfo.Positions);
                }
                else
                {
                    flightDictionary.Add(flightInfo.ICAO, flightInfo);
                }

            });
        }
    }
}
