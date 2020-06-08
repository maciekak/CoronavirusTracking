using System;
using System.Collections.Generic;
using System.Linq;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Repository
{
    public class UserRepository
    {
        public void AddUser(string deviceId, string notificationId, bool isAdmin)
        {
            var user = new User
            {
                AddDate = DateTime.Now,
                DeviceId = deviceId,
                NotificationId = notificationId,
                UserType = isAdmin ? UserType.Doctor : UserType.Normal,
                UserId = Context.UserIdCounter
            };
            Context.UserIdCounter++;
            Context.Users.Add(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return Context.Users.ToList();
        }

        public User GetUserByDeviceId(string deviceId)
        {
            return Context.Users.FirstOrDefault(u => u.DeviceId == deviceId);
        }

        public User GetUserByUserId(int userId)
        {
            return Context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public IEnumerable<User> GetInfected()
        {
            return Context.Users.Where(u => u.InfectionType != InfectionType.Healthy);
        }

        public void Clear()
        {
            Context.Users.Clear();
            Context.UserIdCounter = 1;
        }
    }
}
