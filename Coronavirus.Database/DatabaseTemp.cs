using System;
using System.Collections.Generic;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database
{
    internal static class DatabaseTemp
    {
        public static List<Location> Locations { get; }
        public static List<User> Users { get; }
        public static List<Auth> Auths { get; }

        public static int UserIdCounter = 1;
        public static int LocationIdCounter = 1;

        static DatabaseTemp()
        {
            Locations = new List<Location>();
            Users = new List<User>
            {
                new User
                {
                    AddDate = DateTime.Now,
                    UserId = UserIdCounter,
                    UserType = UserType.Admin,
                    UserUuid = Guid.NewGuid()
                }
            };
            Auths = new List<Auth>
            {
                new Auth {Login = "admin", Password = "admin", UserId = UserIdCounter}
            };
            UserIdCounter++;
        }
    }
}
