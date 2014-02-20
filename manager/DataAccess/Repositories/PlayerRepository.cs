using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}
