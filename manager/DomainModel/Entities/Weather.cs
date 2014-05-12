using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace DomainModel.Entities
{
    public class Weather : Entity
    {
        protected internal Weather() { }

        protected internal Weather(string name, WeatherType type)
        {
            Name = name;
            Type = type;
            MatchCollection = new Collection<Match>();
        }

        public string Name { get; private set; }
        public WeatherType Type { get; private set; }
        public virtual ICollection<Match> MatchCollection { get; private set; }
    }

    public enum WeatherType
    {
        Sun,
        Rain
    }
}
