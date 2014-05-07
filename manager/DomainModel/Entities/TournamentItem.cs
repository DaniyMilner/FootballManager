using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class TournamentItem : Entity
    {
        public TournamentItem() { }
        public TournamentItem(int itemNumber, Tournament tournament, DateTime dateStart)
        {
            ItemNumber = itemNumber;
            Tournament = tournament;
            DateStart = dateStart;
        }

        public int ItemNumber { get; private set; }
        public virtual Tournament Tournament { get; private set; }
        public DateTime DateStart { get; private set; }
    }
}
