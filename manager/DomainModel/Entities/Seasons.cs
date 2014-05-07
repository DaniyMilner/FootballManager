using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DomainModel.Entities
{
    public class Seasons : Entity
    {
        public Seasons() { }
        public Seasons(string title)
        {
            Title = title;
            TournamentCollection = new Collection<Tournament>();
        }

        public string Title { get; private set; }
        [ScriptIgnore]
        public virtual ICollection<Tournament> TournamentCollection { get; private set; }
    }
}
