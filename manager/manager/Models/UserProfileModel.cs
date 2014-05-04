using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace manager.Models
{
    public class UserProfileModel
    {
        public string Password { get; set; }
        public string Skype { get; set; }
        public DateTime? Birthday { get; set; }
        public string City { get; set; }
        public bool Sex { get; set; }
        public string AboutMySelf { get; set; }
    }
}