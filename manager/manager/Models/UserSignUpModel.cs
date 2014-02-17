using System;

namespace manager.Models
{
    public class UserSignUpModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Skype { get; set; }
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public bool Sex { get; set; }
        public string AboutMySelf { get; set; }
        public string ParentId { get; set; }
    }
}