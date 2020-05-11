using System.Collections.Generic;
using System.Linq;
using Coronavirus.Daos;
using Coronavirus.Database.Entities;
using Coronavirus.Database.Managers;
using Coronavirus.Database.Repository;
using CoronavirusTracking.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoronavirusController : ControllerBase
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly LocationRepository _locationRepository = new LocationRepository();
        private readonly InfectionManager _infectionManager = new InfectionManager();

        // POST: api/Coronavirus
        [HttpPost("location")]
        public void AddLocation([FromBody] LocationDto.In location)
        {
            var user = _userRepository.GetUserByDeviceId(location.DeviceId);
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                UserId = user.UserId,
                Time = location.Time
            });
        }

        [HttpPost("locations")]
        public void AddLocations([FromBody] ICollection<LocationDto.In> locations)
        {
            var user = _userRepository.GetUserByDeviceId(locations.First().DeviceId);
            locations
                .ToList()
                .ForEach(location =>
                {
                    _locationRepository.AddLocation(new LocationDao
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        UserId = user.UserId,
                        Time = location.Time
                    });
                });
        }

        // PUT: api/Coronavirus/5
        [HttpGet("locations")]
        public IEnumerable<LocationDto.Out> GetAllLocations()
        {
            return _locationRepository
                .GetLocations()
                .Select(l => new LocationDto.Out
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Time = l.Time
                })
                .ToList();
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet("locations/{userId}")]
        public IEnumerable<LocationDto.Out> GetUserLocations(int userId)
        {
            return _locationRepository
                .GetUserLocations(userId)
                .Select(l => new LocationDto.Out
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Time = l.Time
                })
                .ToList();
        }

        // DELETE: api/ApiWithActions/5
        [HttpPut("infect/{userId}")]
        public void SetInfectedUser(int userId)
        {
            _userRepository.GetUserByUserId(userId).InfectionType = InfectionType.Infected;
            _infectionManager.MarkMetUsersAsInfected(userId);
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet("infected")]
        public IEnumerable<User> GetInfectedUsers()
        {
            return _userRepository
                .GetInfected()
                .ToList();
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet("infected/{deviceId}")]
        public bool GetIfUserIsInfected(string deviceId)
        {
            return _userRepository
                .GetUserByDeviceId(deviceId)
                .InfectionType != InfectionType.Healthy;
        }
    }
}
