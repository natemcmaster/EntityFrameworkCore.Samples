using System;
using System.Collections.Generic;

namespace LoggingSample
{
    public class SampleData
    {
        public static IEnumerable<Ship> GetShips()
            => new List<Ship>
            {
                new Ship
                {
                    Name = "USS Abraham Lincoln",
                    VesselClass = "Frigate",
                    ConstructionDate = new DateTime(1954, 1, 1)
                },
                new Ship
                {
                    Name = "RMS Titanic",
                    VesselClass = "Passenger liner",
                    ConstructionDate = new DateTime(1912, 4, 2)
                },
                new Ship
                {
                    Name = "Black Pearl",
                    VesselClass = "Galleon",
                    ConstructionDate = default(DateTime) // unknown
                }
            };
    }
}