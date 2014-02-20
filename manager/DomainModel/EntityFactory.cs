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

        Player Player(string name, string surname, int age, int weight, int growth, int number, int salary,
            int money, int humor, int condition, User user, Position position, Illness illness, Country country,
            string publicId,
            DateTime createDate);
    }

    public class EntityFactory : IEntityFactory
    {
        public User User(string userName, string password, string email, string parrentId, string skype,
            DateTime birthday, string city, string aboutMySelf, bool sex)
        {
            return new User(userName, password, email, parrentId, skype, birthday, city, aboutMySelf, sex);
        }

        public Player Player(string name, string surname, int age, int weight, int growth, int number, int salary,
            int money, int humor, int condition, User user, Position position, Illness illness, Country country,
            string publicId, DateTime createDate)
        {
            return new Player(name, surname, age, weight, growth, number, salary, money, humor, condition, user, position, illness, country, publicId, createDate);
        }
    }
}
