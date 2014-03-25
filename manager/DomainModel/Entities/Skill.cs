using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DomainModel.Entities
{
    public class Skill:Entity
    {
        protected internal Skill() { }

        protected internal Skill(string name, int ordering) 
        {
            Name = name;
            Ordering = ordering;
            SkillPlayerCollection = new Collection<SkillsPlayer>();
        }

        public string Name { get; private set; }
        public int Ordering { get; private set; }
        [ScriptIgnore]
        public virtual ICollection<SkillsPlayer> SkillPlayerCollection { get; private set; }
    }
}
