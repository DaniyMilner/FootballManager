using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Generator
{
    public class EventManager
    {
        public string StartEvent()
        {
            return "Старт матча";
        }

        public string FinishEvent()
        {
            return "Конец матча";
        }

        public string StartSecondHalf()
        {
            return "Начало 2 тайма";
        }

        public string EndFirstTime()
        {
            return "Конец 1 тайма";
        }
    }
}
