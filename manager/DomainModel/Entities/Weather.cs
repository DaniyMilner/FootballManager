using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Weather : Entity
    {
        protected internal Weather() { }

        protected internal Weather(string name, WeatherType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }
        public WeatherType Type { get; private set; }
    }

    public enum WeatherType
    {
        Sun,
        Rain
    }
}
