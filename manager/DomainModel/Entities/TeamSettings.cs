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
        protected internal TeamSettings(Match match, Arrangement arrangement) 
        {
            Match = match;
            Arrangement = arrangement;
            Settings = String.Empty;
            LineUp = String.Empty;
        }

        public Match Match { get; private set; }
        public Arrangement Arrangement { get; private set; }
        public string Settings { get; private set; }
        public string LineUp { get; private set; }
        public Player PlayerSend { get; private set; }
    }
}
