using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coronavirus.Database.Entities
{
    public class Auth
    {
        [Key]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User User { get; set; }
    }
}
