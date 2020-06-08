using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Spatial;

namespace Coronavirus.Database.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }
        public int LatId { get; set; }
        public int LongId { get; set; }
        public int TimeId { get; set; }
        public DateTime AddDate { get; set; }

        public Cube Cube { get; set; }
    }
}
