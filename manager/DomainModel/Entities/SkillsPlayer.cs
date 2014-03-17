using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class SkillsPlayer : Entity
    {
        protected internal SkillsPlayer() { }

        protected internal SkillsPlayer(Skill skill, Player player)
        {
            Skill = skill;
            Player = player;
            Value = 1;
        }

        public virtual Skill Skill { get; private set; }
        public virtual Player Player { get; private set; }
        public double Value { get; private set; }
    }
}
