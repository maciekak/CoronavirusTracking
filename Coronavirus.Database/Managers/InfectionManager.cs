using System;
using System.Collections.Generic;
using System.Linq;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Managers
{
    public class InfectionManager
    {
        private const int MetersDistance = 10;
        private const int LatitudeDistance = 2;
        private const int LongitudeDistance = 2;
        private readonly TimeSpan _timeDistance = new TimeSpan(0, 0, 10);

        private readonly CoronaContext _coronaContext;

        public InfectionManager(CoronaContext coronaContext)
        {
            _coronaContext = coronaContext;
        }
        public void MarkMetUsersAsInfected(int userId)
        {
            //TODO: make it more efficient, because it is O(n^2) right now
            var infectedUsers = CubeCheckUserWhoHadContact(userId);

            Context.Users.Where(u => infectedUsers.Contains(u.UserId)).ToList().ForEach(u =>
                {
                    u.InfectionType = u.InfectionType == InfectionType.Healthy
                        ? InfectionType.HadContact
                        : u.InfectionType;
                });
        }

        public void MarkUserAsRecovered(int userId)
        {
            var user = Context.Users.First(u => u.UserId == userId);
            if (user.InfectionType == InfectionType.Infected)
            {
                user.InfectionType = InfectionType.Recovered;
            }
        }

        public double GetDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            var d1 = latitude1 * (Math.PI / 180.0);
            var num1 = longitude1 * (Math.PI / 180.0);
            var d2 = latitude2 * (Math.PI / 180.0);
            var num2 = longitude2 * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public List<int> SimpleCheckUserWhoHadContact(int userId)
        {
            var userLocation = Context.Locations.Where(l => l.UserId == userId).ToList();
            return Context.Locations
                .Where(l => l.UserId != userId && userLocation
                                .Any(u => u.Time + _timeDistance > l.Time
                                          && u.Time - _timeDistance < l.Time
                                          && GetDistance(u.Longitude, u.Latitude, l.Longitude, l.Latitude) < MetersDistance))
                .GroupBy(l => l.UserId)
                .Select(l => l.Key)
                .ToList();
        }

        public List<int> CubeCheckUserWhoHadContact(int userId)
        {
            var userLocation = Context.Locations.Where(l => l.UserId == userId).ToList();
            return userLocation.GroupBy(u => new {u.LatId, u.LongId, u.TimeId})
                .ToList()
                .SelectMany(c => Context.Locations
                    .Where(l => l.UserId != userId && (l.LatId == c.Key.LatId && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId + 1
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId + 1
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId - 1 && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId + 1
                                                  || l.LatId == c.Key.LatId && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId + 1
                                                  || l.LatId == c.Key.LatId && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId + 1
                                                  || l.LatId == c.Key.LatId && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId + 1
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId - 1 && l.TimeId == c.Key.TimeId + 1
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId && l.TimeId == c.Key.TimeId + 1
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId - 1
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId
                                                  || l.LatId == c.Key.LatId + 1 && l.LongId == c.Key.LongId + 1 && l.TimeId == c.Key.TimeId + 1))
                    .Where(o => c.Any(p => p.Time + _timeDistance > o.Time
                                                        && p.Time - _timeDistance < o.Time
                                                        && GetDistance(p.Longitude, p.Latitude, o.Longitude, o.Latitude) < MetersDistance))
                    .GroupBy(l => l.UserId)
                    .Select(l => l.Key)
                    .ToList())
                .Distinct()
                .ToList();
        }
    }
}
