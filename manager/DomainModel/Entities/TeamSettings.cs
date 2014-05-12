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

        public void SetSettings(string settings)
        {
            Settings = settings;
        }

        public void SetLineUp(string lineUp)
        {
            LineUp = lineUp;
        }

        public void SetPlayerSend(Guid? playerId)
        {
            PlayerSend = playerId;
        }

        public void SetAttagement(Arrangement arrangement)
        {
            Arrangement = arrangement;
        }
    }
}
