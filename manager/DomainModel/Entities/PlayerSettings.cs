using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class PlayerSettings:Entity
    {
        protected internal PlayerSettings() { }
        protected internal PlayerSettings(Player player, Match match, int index) 
        {
            Player = player;
            Match = match;
            Index = index;
            Settings = String.Empty;
            isCaptain = false;
            isWritable = false;
        }

        public Player Player { get; private set; }
        public Match Match { get; private set; }
        public int Index { get; private set; }
        public string Settings { get; private set; }
        public bool isCaptain { get; private set; }
        public bool isWritable { get; private set; }
    }
}
