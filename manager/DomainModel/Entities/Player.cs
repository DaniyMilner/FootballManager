using System;

namespace DomainModel.Entities
{
    public class Player : Entity
    {
        protected internal Player() { }

        protected internal Player(string name, string surname, int age, int weight, int growth, int number, int salary,
            int money, int humor, int condition, User user, Position position, Illness illness, Country country, string publicId,
            DateTime createDate)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Weight = weight;
            Growth = growth;
            Number = number;
            Salary = salary;
            Money = money;
            Humor = humor;
            Condition = condition;
            User = user;
            Position = position;
            Illness = illness;
            Country = country;
            PublicId = publicId;
            CreateDate = createDate;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Age { get; private set; }
        public int Weight { get; private set; }
        public int Growth { get; private set; }
        public int Number { get; private set; }
        public int Salary { get; private set; }
        public int Money { get; private set; }
        public int Humor { get; private set; }
        public int Condition { get; private set; }
        public User User { get; private set; }
        public Position Position { get; private set; }
        public Illness Illness { get; private set; }
        public Country Country { get; private set; }
        public string PublicId { get; private set; }
        public DateTime CreateDate { get; private set; }
        public Guid? TeamId { get; private set; }
    }
}
