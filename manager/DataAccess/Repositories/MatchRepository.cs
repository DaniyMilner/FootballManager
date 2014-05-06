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
            return GetCollection().ToList();
            return _dataContext.GetSet<Match>().Where(z => z.Result == null || z.Result == string.Empty).ToList();
        }

        public Match GetMatchByPublicId(string publicId)
        {
            return _dataContext.GetSet<Match>().FirstOrDefault(z => z.PublicId == publicId);
        }

        public List<Match> GetTodayMatches()
        {
            var now = DateTime.Now;
            return _dataContext.GetSet<Match>().Where(
                z => z.DateStart.Day == now.Day &&
                z.DateStart.Month == now.Month &&
                z.DateStart.Year == now.Year).ToList();
        }

        public List<Match> GetNotTodayMatches()
        {
            return _dataContext.GetSet<Match>().Where(z => z.DateStart < DateTime.Now && z.DateStart.Day != DateTime.Now.Day)
                .OrderByDescending(z=>z.DateStart).ToList();
        } 
    }
}
