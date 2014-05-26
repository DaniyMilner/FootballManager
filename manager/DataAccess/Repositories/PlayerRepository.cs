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
        public PlayerRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public List<Player> GetAllPlayersByTeamId(Guid id)
        {
            return _dataContext.GetSet<Player>().Where(z => z.TeamId == id).ToList();
        }

        public List<Player> GetCollectionByLineUp(CustomLineUp lineUp)
        {
            return new List<Player>
            {
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.One),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Two),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Three),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Four),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Five),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Six),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Seven),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Eight),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Nine),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Ten),
                _dataContext.GetSet<Player>().FirstOrDefault(z => z.Id == lineUp.Eleven)
            };
        }


        public Player GetPlayerByPublicId(string publicId)
        {
            return _dataContext.GetSet<Player>().FirstOrDefault(p => p.PublicId == publicId);
        }

        public ICollection<Player> GetPlayersByTeamId(Guid teamId)
        {
            return _dataContext.GetSet<Player>().Where(p => p.TeamId == teamId).ToList();
        }
    }
}
