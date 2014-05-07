using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DomainModel.Entities
{
    public class Tournament : Entity
    {
        public Tournament() { }
        public Tournament(string title, Country country, int countItems, Seasons season)
        {
            Title = title;
            Country = country;
            CountItems = countItems;
            Season = season;
            TournamentItemCollection = new Collection<TournamentItem>();
        }

        public string Title { get; private set; }
        public virtual Country Country { get; private set; }
        public int CountItems { get; private set; }
        public virtual Seasons Season { get; private set; }
        [ScriptIgnore]
        public virtual ICollection<TournamentItem> TournamentItemCollection { get; private set; }
    }
}
