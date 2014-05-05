using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DomainModel.Entities
{
    public class Match:Entity
    {
        protected internal Match() { }
        protected internal Match(Guid homeTeamId, Guid guestTeamId, Guid eventLineId, Weather weather, int fansCount, 
            int ticketPrice, DateTime dateStart, string publicId) 
        {
            HomeTeamId = homeTeamId;
            GuestTeamId = guestTeamId;
            EventLineId = eventLineId;
            Weather = weather;
            FansCount = fansCount;
            TicketPrice = ticketPrice;
            DateStart = dateStart;
            Result = String.Empty;
            PublicId = publicId;
            PlayerSettingsCollection = new Collection<PlayerSettings>();
            TeamSettingsCollection = new Collection<TeamSettings>();
            HomeGoal = 0;
            GuestGoal = 0;
        }

        public Guid HomeTeamId { get; private set; }
        public Guid GuestTeamId { get; private set; }
        public Guid EventLineId { get; private set; }
        public virtual Weather Weather { get; private set; }
        public int FansCount { get; private set; }
        public int TicketPrice { get; private set; }
        public DateTime DateStart { get; private set; }
        public string Result { get; private set; }
        public string PublicId { get; private set; }
        public int HomeGoal { get; private set; }
        public int GuestGoal { get; private set; }
        [ScriptIgnore]
        public virtual ICollection<PlayerSettings> PlayerSettingsCollection { get; private set; }
        [ScriptIgnore]
        public virtual ICollection<TeamSettings> TeamSettingsCollection { get; private set; }


        public void SetResult(string result)
        {
            Result = result;
        }

        public void SetGoals(int home, int guest)
        {
            HomeGoal = home;
            GuestGoal = guest;
        }
    }
}
