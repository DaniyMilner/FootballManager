using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel
{
    public interface IEntityFactory
    {
        User User(string userName, string password, string email, string parrentId, string skype,
            DateTime birthday, string city, string aboutMySelf, bool sex);
    }

    public class EntityFactory : IEntityFactory
    {
        public User User(string userName, string password, string email, string parrentId, string skype,
            DateTime birthday, string city, string aboutMySelf, bool sex)
        {
            return new User(userName, password, email, parrentId, skype, birthday, city, aboutMySelf, sex);
        }
    }
}
