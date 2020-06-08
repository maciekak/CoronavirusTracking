using System;
using System.Collections.Generic;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database
{
    internal static class Context
    {
        public static List<Location> Locations { get; }
        public static List<Cube> Cubes { get; }
        public static List<User> Users { get; }
        public static List<Auth> Auths { get; }

        public static int UserIdCounter = 1;
        public static int LocationIdCounter = 1;

        static Context()
        {
            Locations = new List<Location>();
            Cubes = new List<Cube>();
            Users = new List<User>
            {
                new User
                {
                    AddDate = DateTime.Now,
                    UserId = UserIdCounter,
                    UserType = UserType.Admin
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
