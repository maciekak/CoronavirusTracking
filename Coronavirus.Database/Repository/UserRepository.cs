using System;
using System.Collections.Generic;
using System.Linq;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Repository
{
    public class UserRepository
    {
        public void AddUser(string deviceId, bool isAdmin)
        {
            var user = new User
            {
                AddDate = DateTime.Now,
                DeviceId = deviceId,
                IsAdmin = isAdmin,
                UserUuid = Guid.NewGuid(),
                UserId = DatabaseTemp.UserIdCounter
            };
            DatabaseTemp.UserIdCounter++;
            DatabaseTemp.Users.Add(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return DatabaseTemp.Users.ToList();
        }

        public User GetUserByDeviceId(string deviceId)
        {
            return DatabaseTemp.Users.FirstOrDefault(u => u.DeviceId == deviceId);
        }

        public User GetUserByUserId(int userId)
        {
            return DatabaseTemp.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public IEnumerable<User> GetInfected()
        {
            return DatabaseTemp.Users.Where(u => u.IsInfected);
        }
    }
}
