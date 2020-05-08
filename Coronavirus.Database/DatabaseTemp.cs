using System.Collections.Generic;
using Coronavirus.Database.Entities;

namespace Coronavirus.Database
{
    internal static class DatabaseTemp
    {
        public static List<Location> Locations { get; }
        public static List<User> Users { get; }

        public static int UserIdCounter = 1;
        public static int LocationIdCounter = 1;

        static DatabaseTemp()
        {
            Locations = new List<Location>();
            Users = new List<User>();
        }
    }
}
