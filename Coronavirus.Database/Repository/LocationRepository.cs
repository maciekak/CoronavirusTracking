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
        private readonly double _longitudeUpBoundary = 25.0;
        private readonly double _longitudeDownBoundary = 14.0;
        private readonly double _latitudeUpBoundary = 55.0;
        private readonly double _latitudeDownBoundary = 49.00;
        private readonly DateTime _timeUpBoundary = new DateTime(2020, 11, 8, 10, 0, 0);
        private readonly DateTime _timeDownBoundary = new DateTime(2020, 5,8,10,0,0);
        private readonly double _longStep = 0.0007;
        private readonly double _latStep = 0.0006;
        private readonly TimeSpan _timeStep = new TimeSpan(0, 1, 0);

        public void AddLocation(LocationDao location)
        {
            var latId = (int) ((location.Latitude - _latitudeDownBoundary) / _latStep);
            var longId = (int) ((location.Longitude - _longitudeDownBoundary) / _longStep);
            var timeId = (int) ((location.Time - _timeDownBoundary) / _timeStep);
            var cube = Context.Cubes.FirstOrDefault(c =>
                c.LatId == latId && longId == c.LongId && timeId == c.TimeId);
            if (cube == null)
            {
                cube = new Cube
                {
                    LatId = latId,
                    LongId = longId,
                    TimeId = timeId
                };

                Context.Cubes.Add(cube);
            }

            var dbLocation = new Location
            {
                AddDate = DateTime.Now,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                LatId = cube.LatId,
                LongId = cube.LongId,
                TimeId = cube.TimeId,
                LocationId = Context.LocationIdCounter,
                Time = location.Time,
                UserId = location.UserId
            };
            Context.LocationIdCounter++;
            Context.Locations.Add(dbLocation);
        }

        public IEnumerable<LocationDao> GetLocations()
        {
            return Context.Locations
                .Select(l => new LocationDao
                {
                    AddDate = l.AddDate,
                    Id = l.LocationId,
                    Time = l.Time,
                    UserId = l.UserId,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude
                });
        }

        public IEnumerable<LocationDao> GetUserLocations(int userId)
        {
            return Context.Locations
                .Where(l => l.UserId == userId)
                .Select(l => new LocationDao
                {
                    AddDate = l.AddDate,
                    Id = l.LocationId,
                    Time = l.Time,
                    UserId = l.UserId,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude
                });
        }

        public void Clear()
        {
            Context.Locations.Clear();
            Context.LocationIdCounter = 1;
        }
    }
}
