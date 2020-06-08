using System;
using System.ComponentModel.DataAnnotations;

namespace Coronavirus.Database.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string DeviceId { get; set; }
        public string NotificationId { get; set; }
        public InfectionType InfectionType { get; set; }
        public UserType UserType { get; set; }
        public DateTime AddDate { get; set; }
    }
}
