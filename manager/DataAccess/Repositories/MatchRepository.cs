using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public List<Match> GetAllNotGeneratedMatches()
        {
            return _dataContext.GetSet<Match>().Where(z => z.Result == null || z.Result == string.Empty).ToList();
        }

        public Match GetMatchByPublicId(string publicId)
        {
            return _dataContext.GetSet<Match>().FirstOrDefault(z => z.PublicId == publicId);
        }
    }
}
