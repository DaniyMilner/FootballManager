using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class MatchEventItem
    {
        public int LineIndex;
        public int CellIndex;
        public Guid? AttackPlayer;
        public Guid? DefendPlayer;
        public Guid? Goalkeeper;

        public MatchEventItemType EventType;

        public MatchEventItem() { }

        public MatchEventItem(int lineIndex, int cellIndex, Guid? attack, Guid? defend, Guid? keeper, MatchEventItemType eventItemType)
        {
            LineIndex = lineIndex;
            CellIndex = cellIndex;
            AttackPlayer = attack;
            DefendPlayer = defend;
            Goalkeeper = keeper;
            EventType = eventItemType;
        }

        public MatchEventItem(MatchEventItemType itemType)
        {
            LineIndex = 0;
            CellIndex = 0;
            AttackPlayer = null;
            DefendPlayer = null;
            Goalkeeper = null;
            EventType = itemType;
        }
    }
}
