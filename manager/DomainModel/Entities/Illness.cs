using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace DomainModel.Entities
{
    public class Illness : Entity
    {
        protected internal Illness() { }

        protected internal Illness(string illnessName, int timeForRecovery)
        {
            IllnessName = illnessName;
            TimeForRecovery = timeForRecovery;
            PlayerCollection = new Collection<Player>();
        }

        public string IllnessName { get; private set; }
        public int TimeForRecovery { get; private set; }
        [ScriptIgnore]
        public virtual ICollection<Player> PlayerCollection { get; set; }
    }
}
