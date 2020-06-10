using System;
using System.Collections.Generic;
using System.Linq;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database.Repository
{
    public class UserRepository
    {
        private readonly CoronaContext _coronaContext;

        public UserRepository(CoronaContext coronaContext)
        {
            _coronaContext = coronaContext;
        }

        public void AddUser(string deviceId, string notificationId, bool isAdmin)
        {
            var user = new User
            {
                AddDate = DateTime.Now,
                DeviceId = deviceId,
                NotificationId = notificationId,
                UserType = isAdmin ? UserType.Doctor : UserType.Normal
            };
            _coronaContext.Users.Add(user);
            _coronaContext.SaveChanges();
        }

        public IEnumerable<User> GetUsers()
        {
            return _coronaContext.Users.ToList();
        }

        public User GetUserByDeviceId(string deviceId)
        {
            return _coronaContext.Users.FirstOrDefault(u => u.DeviceId == deviceId);
        }

        public User GetUserByUserId(int userId)
        {
            return _coronaContext.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public IEnumerable<User> GetInfected()
        {
            return _coronaContext.Users.Where(u => u.InfectionType != InfectionType.Healthy);
        }

        public void Clear()
        {
            Context.Users.Clear();
            Context.UserIdCounter = 1;
        }
    }
}
