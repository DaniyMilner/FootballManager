using System;
using Infrastructure;

namespace DomainModel.Entities
{
    public class User : Entity
    {
        protected internal User() { }

        protected internal User(string userName, string password, string email, string parrentId, string skype,
            DateTime? birthday, string city, string aboutMySelf, bool sex)
        {
            Email = email;
            UserName = userName;
            PasswordHash = Cryptography.GetHash(password);
            Skype = skype;
            ParentId = parrentId;
            Birthday = birthday;
            City = city;
            AboutMySelf = aboutMySelf;
            Sex = sex;
            Language = "ru";
        }

        public string Email { get; private set; }

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

        public DateTime? Birthday { get; private set; }

        public void UpdateBirthday(DateTime birthday)
        {
            Birthday = birthday;
        }

        public string City { get; private set; }

        public void UpdateCity(string city)
        {
            City = city;
        }

        public string AboutMySelf { get; private set; }

        public void UpdateAboutMySelf(string aboutMySelf)
        {
            AboutMySelf = aboutMySelf;
        }

        public bool Sex { get; private set; }

        public void UpdateSex(bool sex)
        {
            Sex = sex;
        }

        public string Language { get; private set; }

        public void UpdateLanguage(string language)
        {
            Language = language;
        }

    }
}
