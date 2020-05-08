using System;

namespace CoronavirusTracking.Dtos
{
    public abstract class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }

        public class In : LocationDto
        {
            public string DeviceId { get; set; }
        }

        public class Out : LocationDto
        {
            public int Id { get; set; }
            public int UserId { get; set; }
        }
    }
}
