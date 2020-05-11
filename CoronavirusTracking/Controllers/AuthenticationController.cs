using System.Collections.Generic;
using Coronavirus.Database.Entities;
using Coronavirus.Database.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CoronavirusTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserRepository _userRepository = new UserRepository();

        public AuthenticationController(IMemoryCache memoryCache)
        {

        }

        // GET: api/Authentication/5
        [HttpGet("users")]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        // POST: api/Authentication
        [HttpPost("admin")]
        public void AddAdmin([FromBody] string deviceId)
        {
            _userRepository.AddUser(deviceId, true);
        }

        // PUT: api/Authentication/5
        [HttpPost("user")]
        public void AddUser([FromBody] string deviceId)
        {
            _userRepository.AddUser(deviceId, false);
        }
    }
}
