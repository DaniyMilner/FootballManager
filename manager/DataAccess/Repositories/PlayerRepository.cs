using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public List<Player> GetAllPlayersByTeamId(Guid id)
        {
            return _dataContext.GetSet<Player>().Where(z => z.TeamId == id).ToList();
        }
    }
}
