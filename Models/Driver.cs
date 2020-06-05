using System.Collections.Generic;

namespace CodeKata.Models
{
    public class Driver
    {
        public string Name { get; set; }
        public List<Trip> Trips { get; set; }

        public Driver()
        {
            Trips = new List<Trip>();
        }
    }
}
