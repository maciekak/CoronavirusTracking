using System;
using System.Spatial;

namespace Coronavirus.Database.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public GeographyPosition Geolocation { get; set; }
        public DateTime Time { get; set; }
        public DateTime AddDate { get; set; }
    }
}
