using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class TeamSettings:Entity
    {
        protected internal TeamSettings() { }
        protected internal TeamSettings(Match match, Arrangement arrangement, Team team) 
        {
            Team = team;
            Match = match;
            Arrangement = arrangement;
            Settings = String.Empty;
            LineUp = String.Empty;
        }

        public virtual Team Team { get; private set; }
        public virtual Match Match { get; private set; }
        public virtual Arrangement Arrangement { get; private set; }
        public string Settings { get; private set; }
        public string LineUp { get; private set; }
        public Guid? PlayerSend { get; private set; }
    }
}
