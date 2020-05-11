using System;
using System.Collections.Generic;
using System.Linq;
using System.Spatial;
using Coronavirus.Daos;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Repository
{
    public class LocationRepository
    {
        public void AddLocation(LocationDao location)
        {
            var dbLocation = new Location
            {
                AddDate = DateTime.Now,
                Geolocation = new GeographyPosition(location.Latitude, location.Longitude),
                Id = DatabaseTemp.LocationIdCounter,
                Time = location.Time,
                UserId = location.UserId
            };
            DatabaseTemp.LocationIdCounter++;
            DatabaseTemp.Locations.Add(dbLocation);
        }

        public IEnumerable<LocationDao> GetLocations()
        {
            return DatabaseTemp.Locations
                .Select(l => new LocationDao
                {
                    AddDate = l.AddDate,
                    Id = l.Id,
                    Time = l.Time,
                    UserId = l.UserId,
                    Latitude = l.Geolocation.Latitude,
                    Longitude = l.Geolocation.Longitude
                });
        }

        public IEnumerable<LocationDao> GetUserLocations(int userId)
        {
            return DatabaseTemp.Locations
                .Where(l => l.UserId == userId)
                .Select(l => new LocationDao
                {
                    AddDate = l.AddDate,
                    Id = l.Id,
                    Time = l.Time,
                    UserId = l.UserId,
                    Latitude = l.Geolocation.Latitude,
                    Longitude = l.Geolocation.Longitude
                });
        }

        public void Clear()
        {
            DatabaseTemp.Locations.Clear();
            DatabaseTemp.LocationIdCounter = 1;
        }
    }
}
