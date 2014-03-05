using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Team : Entity
    {
        protected internal Team() { }

        protected internal Team(string name, string shortname, string logo, Country country, string stadium, int year)
        {
            Name = name;
            ShortName = shortname;
            Logo = logo;
            Country = country;
            Stadium = stadium;
            Year = year;
        }

        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string Logo { get; private set; }
        public Guid? CoachId { get; private set; }
        public Guid? AssistantId { get; private set; }
        public Country Country { get; private set; }
        public string Stadium { get; private set; }
        public int Year { get; private set; }
    }
}
