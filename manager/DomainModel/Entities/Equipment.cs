using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DomainModel.Entities
{
    public class Equipment : Entity
    {
        public Equipment()
        {
        }

        public Equipment(string name, double price, int countOfMatch, int amountOfSkills,
            EquipmentType type, WeatherType weatherType, int index)
        {
            Name = name;
            Price = price;
            CountOfMatch = countOfMatch;
            AmountOfSkills = amountOfSkills;
            PlayersCollection = new Collection<Player>();
            Type = type;
            WeatherType = weatherType;
            Index = index;
        }

        public string Name { get; private set; }
        public double Price { get; private set; }
        public int CountOfMatch { get; private set; }
        public int AmountOfSkills { get; private set; }
        public EquipmentType Type { get; private set; }
        public WeatherType WeatherType { get; private set; }
        public int Index { get; private set; }

        public ICollection<Player>  PlayersCollection { get; set; }
    }

    public enum EquipmentType
    {
        Gloves,
        Boots,
        Shields
    }
}
