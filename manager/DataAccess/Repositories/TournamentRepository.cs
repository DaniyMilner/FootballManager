using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class TournamentRepository : Repository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public List<Tournament> GeTournamentsBySeasonId(Guid id)
        {
            return _dataContext.GetSet<Tournament>().Where(z => z.Season.Id == id).ToList();
        }

        public Tournament GeTournamentByPublicId(string publicId)
        {
            return _dataContext.GetSet<Tournament>().FirstOrDefault(z => z.PublicId == publicId);
        }
    }
}
