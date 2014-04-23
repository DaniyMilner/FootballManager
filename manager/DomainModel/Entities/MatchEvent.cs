using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class MatchEvent
    {
        public int Minute;
        public bool IsHome;
        public List<MatchEventItem> EventsLine;

        public MatchEvent(){}

        public MatchEvent(int minute, bool isHome, List<MatchEventItem> eventsLine)
        {
            Minute = minute;
            IsHome = isHome;
            EventsLine = eventsLine;
        }
    }
}
