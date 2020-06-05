using CodeKata.Models;
using CodeKata.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeKata
{

    public class FileCommandParser : ICommandParser<Driver>
    {
        private readonly IRepository<Driver> repository;

        private string file { get; }

        public FileCommandParser(string file, IRepository<Driver> repository)
        {
            this.file = file;
            this.repository = repository;
        }
        public IRepository<Driver> GetData()
        {
            if (!File.Exists(this.file))
            {
                throw new FileNotFoundException("Input file was not found", file);
            }

            using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Only non empty lines
                    if (line.Trim().Length > 0)
                    {
                        var sublines = line.Split(' ');

                        switch (sublines[0].ToLowerInvariant())
                        {
                            case "driver":

                                var driver = ParseDriver(sublines);
                                if(driver != null)
                                {
                                    repository.Add(driver);
                                }
                                
                                break;
                            case "trip":
                                var result = ParseTrip(sublines);

                                var savedDriver = repository.GetByName(result.Item1);

                                if (savedDriver != null)
                                {
                                    savedDriver.Trips.Add(result.Item2);
                                }

                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return repository;
        }

        private Driver ParseDriver(string[] line) 
        {
            try
            {
                if (line.Length == 2)
                {
                    return new Driver() { Name = line[1] };
                }
            }
            catch{}

            return null;
        }

        private Tuple<string,Trip> ParseTrip(string[] line) 
        {
            try
            {
                if (line.Length == 5)
                {
                    // Trip Dan 07:15 07:45 17.3
                    double distance;
                    double.TryParse(line[4], out distance);
                    distance = Math.Round(distance,0, MidpointRounding.AwayFromZero);
                    var trip = new Trip
                    {
                        Start = ParseSpan(line[2]),
                        End = ParseSpan(line[3]),
                        Distance = distance
                    };

                    var parseResult = new Tuple<string, Trip>(line[1], trip);
                    return parseResult;
                }
                
            }
            catch{}

            return null;
            
        }

        private TimeSpan ParseSpan(string value)
        {
            try
            {
                var values = value.Split(':');
                return new TimeSpan(int.Parse(values[0]), int.Parse(values[1]), 0);
            }
            catch
            {
                return new TimeSpan(0, 0, 0);
            }
            
        }

    }
}
