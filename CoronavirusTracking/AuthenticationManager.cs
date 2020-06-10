using System;
using System.Security.Cryptography;
using System.Text;
using Coronavirus.Database;
using Coronavirus.Database.Entities;
using Coronavirus.Database.Repository;
using CoronavirusTracking.Dtos;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace CoronavirusTracking
{
    public class AuthenticationManager
    {
        private const string Secret = "1234";

        private readonly IMemoryCache _cache;
        private readonly AuthRepository _authRepository;
        private readonly UserRepository _userRepository;

        public AuthenticationManager(IMemoryCache memoryCache, CoronaContext coronaContext)
        {
            _cache = memoryCache;
            _authRepository = new AuthRepository(coronaContext);
            _userRepository = new UserRepository(coronaContext);
        }

        public string GetToken(string login, string password)
        {
            if (_authRepository.UserExists(login, password))
            {
                using var sha256 = SHA256.Create();
                var uuid = Guid.NewGuid().ToString();

                var hashValue = sha256.ComputeHash(Encoding.ASCII.GetBytes(Secret + ":" + login + ":" + uuid));
                var token = GetStringByteArray(hashValue);

                _cache.Set(token, new Tuple<string, string>(login, uuid));

                return token;
            }
            else
            {
                return null;
            }
        }

        public bool IfUserIs(string token, UserType typeOfUser)
        {
            if (!_cache.TryGetValue(token, out var value)) return false;
            var (login,  uuid) = (Tuple<string, string>) value;

            using var sha256 = SHA256.Create();
            var hashValue = sha256.ComputeHash(Encoding.ASCII.GetBytes(Secret + ":" + login + ":" + uuid));
            if (GetStringByteArray(hashValue) != token) return false;

            var userId = _authRepository.GetUserIdByLogin(login);
            var user = _userRepository.GetUserByUserId(userId);
            return user.UserType == typeOfUser;
        }

        private static string GetStringByteArray(byte[] array)
        {
            var result = "";
            for (int i = 0; i < array.Length; i++)
            {
                result += $"{array[i]:X2}";
            }

            return result;
        }

        public void Logout(string token)
        {
            _cache.Remove(token);
        }
    }
}
