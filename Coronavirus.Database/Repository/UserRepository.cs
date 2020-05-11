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
                UserType = isAdmin ? UserType.Doctor : UserType.Normal,
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
            return DatabaseTemp.Users.Where(u => u.InfectionType != InfectionType.Healthy);
        }

        public void Clear()
        {
            DatabaseTemp.Users.Clear();
            DatabaseTemp.UserIdCounter = 1;
        }
    }
}
