using System.Collections.Generic;
using Coronavirus.Database.Entities;
using Coronavirus.Database.Repository;
using CoronavirusTracking.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;

namespace CoronavirusTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly AuthRepository _authRepository = new AuthRepository();
        private readonly AuthenticationManager _authenticationManager;

        public AuthenticationController(IMemoryCache memoryCache)
        {
            _authenticationManager = new AuthenticationManager(memoryCache);
        }

        // GET: api/Authentication/5
        [HttpGet("users")]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        // POST: api/Authentication
        [HttpPost("admin")]
        public void AddAdmin([FromBody] LoginPassword.In loginPassword)
        {
            if (!Request.Headers.TryGetValue("Authentication", out var token)) return;
            if(!_authenticationManager.IfUserIs(token, UserType.Admin)) return;
            _authRepository.AddAdmin(loginPassword.Login, loginPassword.Password, loginPassword.DeviceId);
        }

        // PUT: api/Authentication/5
        [HttpPost("user")]
        public void AddUser([FromBody] AddUserDto user)
        {
            _userRepository.AddUser(user.DeviceId, user.NotificationId, false);
        }

        [HttpPost("login")]
        public bool Login([FromBody] LoginPassword.In loginPassword)
        {
            var token = _authenticationManager.GetToken(loginPassword.Login, loginPassword.Password);
            if (!string.IsNullOrEmpty(token))
            {
                return Response.Headers.TryAdd("Authentication", token);
            }
            else
            {
                return false;
            }
        }

        [HttpPost("logout")]
        public bool LogOut([FromBody] LoginPassword.In loginPassword)
        {
            if (!Request.Headers.ContainsKey("Authentication")) return false;
            _authenticationManager.Logout(Request.Headers["Authentication"]);
            return Request.Headers.Remove("Authentication");
        }
    }
}
