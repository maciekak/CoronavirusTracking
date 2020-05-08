using System;

namespace Coronavirus.Daos
{
    public class LocationDao
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }
        public DateTime AddDate { get; set; }
    }
}
