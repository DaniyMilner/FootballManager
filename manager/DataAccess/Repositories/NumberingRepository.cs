using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class NumberingRepository : Repository<Numbering>, INumberingRepository
    {
        public NumberingRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public Numbering GetNextNumber(NumberingType type)
        {
            return _dataContext.GetSet<Numbering>().SingleOrDefault(n => n.Type == type);
        }
    }
}
