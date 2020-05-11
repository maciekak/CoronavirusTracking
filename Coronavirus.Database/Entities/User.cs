using System;

namespace Coronavirus.Database.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string DeviceId { get; set; }
        public Guid UserUuid { get; set; }
        public InfectionType InfectionType { get; set; }
        public UserType UserType { get; set; }
        public DateTime AddDate { get; set; }
    }
}
