using CodeKata.Models;
using CodeKata.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeKata.Procesors
{
    public interface IProcessor<TResults>
    {
        void GetResults();
    }

    public class DriverProccesor : IProcessor<TripsReportResult>
    {
        private readonly double minSpeed = 5;
        private readonly double maxSpeed = 100;
        public DriverProccesor(IRepository<Driver> repository) {
            this.repository = repository;
        }

        private IRepository<Driver> repository { get; }

        public void GetResults()
        {
            var trips = new List<TripData>();
            var invalidtrips = new List<TripsReportResult>();

            repository.GetAll().ForEach(driver => 
            {
                if (driver.Trips.Any())
                {
                    driver.Trips.ForEach(trip =>
                    {
                        trips.Add(CalculateSpped(driver.Name, trip));
                    });
                }
                else 
                {
                    invalidtrips.Add(new TripsReportResult() { Name = driver.Name, Miles = 0, MilesPerHour = 0, ValidTrip = false});
                }
                
            });

            var results = trips
                .Where(t => t.ValidTrip)
                .GroupBy(t => t.Name)
                .Select(trip => new TripsReportResult()
                {
                    Miles = trip.Sum(m => Math.Round(m.Miles, 0, MidpointRounding.AwayFromZero)),
                    Name = trip.Key,
                    MilesPerHour = trip.Average(m => Math.Round( m.MilesPerHour,0, MidpointRounding.AwayFromZero))
                })                
                .ToList();

            results.AddRange(invalidtrips);


            results.OrderByDescending(m => m.Miles).ToList().ForEach(t => Console.WriteLine(t));
        }

        

        private TripData CalculateSpped(string name, Trip trip)
        {
            var time = trip.End - trip.Start;
            var mph = trip.Distance / Math.Round(time.TotalHours, 2);
            mph = Math.Round(mph, 0, MidpointRounding.AwayFromZero);
            var result = new TripData()
            {
                Name = name,
                Hours = time.TotalHours,
                Miles = trip.Distance,
                MilesPerHour = mph,
                ValidTrip = mph > minSpeed && mph < maxSpeed
            };

            return result;
        }

    }
}
