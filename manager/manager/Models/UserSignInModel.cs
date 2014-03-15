using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace manager.Models
{
    public class UserSignInModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}