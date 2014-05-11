using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public Team GeTeamByPublicId(string publicId)
        {
            return _dataContext.GetSet<Team>().SingleOrDefault(t => t.ShortName == publicId);
        }
    }
}
