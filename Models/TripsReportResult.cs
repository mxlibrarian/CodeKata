using System;

namespace CodeKata.Models
{
    public class TripsReportResult
    {
        public string Name { get; set; }
        public double Miles { get; set; }
        public double MilesPerHour { get; set; }
        public bool ValidTrip { get; set; }

        public override string ToString()
        {
            if (Miles > 0)
            {
                
                return $"{Name}: {Miles} miles @ {Math.Round(MilesPerHour, 0, MidpointRounding.AwayFromZero)} mhp";
            }
            else 
            {
                return $"{Name}: {Miles} miles";
            }
        }
    }
}
