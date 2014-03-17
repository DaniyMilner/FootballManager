using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class TeamSettingsRepository : Repository<TeamSettings>, ITeamSettingsRepository
    {
        public TeamSettingsRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
