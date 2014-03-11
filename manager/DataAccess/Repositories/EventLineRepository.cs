using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class EventLineRepository : Repository<EventLine>, IEventLineRepository
    {
        public EventLineRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
