using System;
using System.Collections.Generic;
using System.Linq;
using Coronavirus.Daos;
using Coronavirus.Database;
using Coronavirus.Database.Entities;
using Coronavirus.Database.Managers;
using Coronavirus.Database.Repository;
using CoronavirusTracking.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly LocationRepository _locationRepository;
        private readonly CubeRepository _cubeRepository;
        private readonly InfectionManager _infectionManager;
        private readonly CoronaContext _coronaContext;

        private readonly double _longitudeUpBoundary = 25.0;
        private readonly double _longitudeDownBoundary = 14.0;
        private readonly double _latitudeUpBoundary = 55.0;
        private readonly double _latitudeDownBoundary = 49.00;
        private readonly DateTime _timeUpBoundary = new DateTime(2020, 11, 8, 10, 0, 0);
        private readonly DateTime _timeDownBoundary = new DateTime(2020, 5, 8, 10, 0, 0);
        private readonly double _longStep = 0.0007;
        private readonly double _latStep = 0.0006;
        private readonly TimeSpan _timeStep = new TimeSpan(0, 1, 0);

        public TestController(CoronaContext coronaContext)
        {
            _coronaContext = coronaContext;
            _infectionManager = new InfectionManager(_coronaContext);
            _cubeRepository = new CubeRepository(_coronaContext);
            _locationRepository = new LocationRepository(_coronaContext);
            _userRepository = new UserRepository(_coronaContext);
        }

        private List<DateTime> GetTimes()
        {
            return new List<DateTime>
            {
                new DateTime(2020, 5, 11, 1, 1, 1),
                new DateTime(2020, 5, 11, 1, 2, 1),
                new DateTime(2020, 5, 11, 1, 3, 1),
                new DateTime(2020, 5, 11, 1, 4, 1),
            };
        }

        [HttpGet("clear")]
        public void Clear()
        {
            _userRepository.Clear();
            _locationRepository.Clear();
            _cubeRepository.Clear();
        }

        [HttpPost("generate")]
        public int Generate([FromBody] GenerationDto dto)
        {
            var a = _infectionManager.GetDistance(_longitudeDownBoundary + _longStep, _latitudeDownBoundary + _latStep,
                _longitudeDownBoundary, _latitudeDownBoundary);
            var random = new Random();
            var contacted = new List<int>();
            var i = 0;
            for (; contacted.Count < dto.ContactQuantity; i++)
            {
                var beginDate = new DateTime(2020, 6, 10, 10, 10, 10);
                var endDate = beginDate.AddSeconds(dto.LengthInSeconds);
                _userRepository.AddUser(i.ToString(), i.ToString(), false);
                var userId = _userRepository.GetUserByDeviceId(i.ToString()).UserId;

                var lastPosition = (random.NextDouble() * (dto.LatTo - dto.LatFrom) + dto.LatFrom,
                    random.NextDouble() * (dto.LongTo - dto.LongFrom) + dto.LongFrom);
                while (beginDate < endDate)
                {
                    _locationRepository.AddLocation(new LocationDao
                    {
                        Latitude = lastPosition.Item1,
                        Longitude = lastPosition.Item2,
                        UserId = userId,
                        Time = beginDate
                    });
                    lastPosition = (lastPosition.Item1 + dto.StepLength * (random.NextDouble() * 2 - 1),
                        lastPosition.Item2 + dto.StepLength * (random.NextDouble() * 2 - 1));
                    beginDate = beginDate.AddSeconds(dto.StepInSeconds);
                }

                var contact = _infectionManager.SimpleCheckUserWhoHadContact(userId);
                if (contact.Count > 0)
                {
                    contacted.Add(userId);
                    contacted.AddRange(contact);
                    contacted = contacted.Distinct().ToList();
                }

            }

            return i;
        }

        [HttpGet("check/all")]
        public string CheckIfAnyoneIsInfected()
        {
            var users = _userRepository.GetUsers().Select(u => u.UserId).ToList();
            var start = DateTime.Now;
            var contacted1 = users
                .SelectMany(u => _infectionManager.SimpleCheckUserWhoHadContact(u))
                .Distinct()
                .ToList();
            var diff1 = (DateTime.Now - start).TotalMilliseconds;

            start = DateTime.Now;
            var contacted2 = users
                .SelectMany(u => _infectionManager.CubeCheckUserWhoHadContact(u))
                .Distinct()
                .ToList();
            var diff2 = (DateTime.Now - start).TotalMilliseconds;
            return $"Basic: {contacted1.Count} - {diff1}\nCubes: {contacted2.Count} - {diff2}";
        }
    }
}
