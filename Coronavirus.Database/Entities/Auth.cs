using System;
using System.Collections.Generic;
using System.Text;

namespace Coronavirus.Database.Entities
{
    public class Auth
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
