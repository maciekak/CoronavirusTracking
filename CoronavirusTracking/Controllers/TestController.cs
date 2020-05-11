using System;
using System.Collections.Generic;
using Coronavirus.Daos;
using Coronavirus.Database.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly LocationRepository _locationRepository = new LocationRepository();

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
        }

        // GET: api/Test
        [HttpGet("contact/none")]
        public void TestNone()
        {
            _userRepository.AddUser("1", false);
            _userRepository.AddUser("2", false);
            _userRepository.AddUser("3", false);
            _userRepository.AddUser("4", false);

            var id1 = _userRepository.GetUserByDeviceId("1").UserId;
            var id2 = _userRepository.GetUserByDeviceId("2").UserId;
            var id3 = _userRepository.GetUserByDeviceId("3").UserId;
            var id4 = _userRepository.GetUserByDeviceId("4").UserId;

            var times = GetTimes();
            var id = id1;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 2,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 2,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id2;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 5,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 6,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 5,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 6,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id3;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 10,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 13,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 16,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 19,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id4;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 16,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 20,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 10,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 13,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


        }

        [HttpGet("contact/some")]
        public void TestSome()
        {
            _userRepository.AddUser("5", false);
            _userRepository.AddUser("6", false);
            _userRepository.AddUser("7", false);
            _userRepository.AddUser("8", false);

            var id1 = _userRepository.GetUserByDeviceId("5").UserId;
            var id2 = _userRepository.GetUserByDeviceId("6").UserId;
            var id3 = _userRepository.GetUserByDeviceId("7").UserId;
            var id4 = _userRepository.GetUserByDeviceId("8").UserId;

            var times = GetTimes();
            var id = id1;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 2,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 2,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id2;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 6,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 7,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 6,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 7,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id3;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 5,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 7,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 9,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 11,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id4;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 14,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 14,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 14,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 14,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });
            
        }

        [HttpGet("contact/all")]
        public void TestAll()
        {
            _userRepository.AddUser("9", false);
            _userRepository.AddUser("10", false);
            _userRepository.AddUser("11", false);
            _userRepository.AddUser("12", false);

            var id1 = _userRepository.GetUserByDeviceId("9").UserId;
            var id2 = _userRepository.GetUserByDeviceId("10").UserId;
            var id3 = _userRepository.GetUserByDeviceId("11").UserId;
            var id4 = _userRepository.GetUserByDeviceId("12").UserId;

            var times = GetTimes();
            var id = id1;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id2;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 2,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 3,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 4,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id3;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 4,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 3,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 8,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });


            id = id4;
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 9,
                Longitude = 1,
                UserId = id,
                Time = times[0]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 3.5,
                Longitude = 1,
                UserId = id,
                Time = times[1]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 9.5,
                Longitude = 1,
                UserId = id,
                Time = times[2]
            });
            _locationRepository.AddLocation(new LocationDao
            {
                Latitude = 1.5,
                Longitude = 1,
                UserId = id,
                Time = times[3]
            });
        }
    }
}
