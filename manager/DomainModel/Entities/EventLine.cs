using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class EventLine : Entity
    {
        protected internal EventLine() { }
        protected internal EventLine(Guid lineId, Player player, int minute, EventLineType type)
        {
            LineId = lineId;
            Player = player;
            Minute = minute;
            Type = type;
        }

        public Guid LineId { get; private set; }
        public Player Player { get; private set; }
        public int Minute { get; private set; }
        public EventLineType Type { get; private set; }
    }

    public enum EventLineType
    {
        Goal,
        Yellow,
        Red
    }
}
