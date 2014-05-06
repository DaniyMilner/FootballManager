using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public Skill GetSkillByOrdering(int order)
        {
            return _dataContext.GetSet<Skill>().SingleOrDefault(s => s.Ordering == order);
        }
    }
}
