using System;
using System.Linq;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Managers
{
    public class InfectionManager
    {
        private const int LatitudeDistance = 2;
        private const int LongitudeDistance = 2;
        private readonly TimeSpan _timeDistance = new TimeSpan(0, 1, 10); 

        public void MarkMetUsersAsInfected(int userId)
        {
            //TODO: make it more efficient, because it is O(n^2) right now
            var userLocation = DatabaseTemp.Locations.Where(l => l.UserId == userId).ToList();
            var infectedUsers = DatabaseTemp.Locations
                .Where(l => l.UserId != userId && userLocation
                                .Any(u => u.Time + _timeDistance > l.Time
                                          && u.Time - _timeDistance < l.Time
                                          && u.Geolocation.Latitude + LatitudeDistance > l.Geolocation.Latitude
                                          && u.Geolocation.Latitude - LatitudeDistance < l.Geolocation.Latitude
                                          && u.Geolocation.Longitude + LongitudeDistance > l.Geolocation.Longitude
                                          && u.Geolocation.Longitude - LongitudeDistance < l.Geolocation.Longitude))
                .GroupBy(l => l.UserId)
                .Select(l => l.Key)
                .ToList();

            DatabaseTemp.Users.Where(u => infectedUsers.Contains(u.UserId)).ToList().ForEach(u =>
                {
                    u.InfectionType = u.InfectionType == InfectionType.Healthy
                        ? InfectionType.HadContact
                        : u.InfectionType;
                });
        }
    }
}
