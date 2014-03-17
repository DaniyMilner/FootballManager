using DomainModel.Entities;
using DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class IllnessRepository :Repository<Illness>, IIllnessRepository
    {
        public IllnessRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public Illness GetIllnessByName(string name)
        {
            return _dataContext.GetSet<Illness>().SingleOrDefault(i => i.IllnessName == name);
        }
    }
}
