using System;

namespace Coronavirus.Database.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string DeviceId { get; set; }
        public Guid UserUuid { get; set; }
        public bool IsInfected { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime AddDate { get; set; }
    }
}
