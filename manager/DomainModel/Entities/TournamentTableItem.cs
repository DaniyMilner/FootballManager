using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class TournamentTableItem
    {
        public TournamentTableItem() { }

        public TournamentTableItem(Guid teamId, string teamName, string teamShortName)
        {
            TeamId = teamId;
            TeamName = teamName;
            TeamShortName = teamShortName;
            Games = Win = Draw = Lost = Goals = LostGoals = DifferenceGoals = Points = 0;
        }

        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamShortName { get; set; }
        public int Games { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int Goals { get; set; }
        public int LostGoals { get; set; }
        public int DifferenceGoals { get; set; }
        public int Points { get; set; }
    }
}
