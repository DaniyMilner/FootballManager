using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class CustomTeamSettings
    {
        public Guid Corner { get; set; }
        public Guid Freekick { get; set; }
        public Guid Penalty { get; set; }

        public Player PlayerCorner { get; set; }
        public Player PlayerFreekick { get; set; }
        public Player PlayerPenalty { get; set; }
    }
}
