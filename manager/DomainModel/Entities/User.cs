using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace DomainModel.Entities
{
    public class User : Entity
    {
        protected internal User() { }

        protected internal User(string userName, string password, string email, string parrentId, string skype)
        {
            Email = email;
            UserName = userName;
            PasswordHash = Cryptography.GetHash(password);
            Skype = skype;
            ParentId = parrentId;
        }

        public string Email { get; private set; }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public string PasswordHash { get; private set; }

        public bool VerifyPassword(string password)
        {
            return Cryptography.VerifyHash(password, PasswordHash);
        }

        public void UpdatePassword(string oldPassword, string newPassword)
        {
            if (VerifyPassword(oldPassword))
            {
                PasswordHash = Cryptography.GetHash(newPassword);
            }
        }

        public string UserName { get; private set; }
        public string ParentId { get; private set; }

        public string Skype { get; private set; }

        public void UpdateSkype(string skype)
        {
            Skype = skype;
        }

    }
}
