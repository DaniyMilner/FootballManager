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

        public List<Match> GetTodayMatches()
        {
            var now = DateTime.Now;
            var tours = _dataContext.GetSet<TournamentItem>().Where(z => z.DateStart.Day == now.Day &&
                                                                         z.DateStart.Month == now.Month &&
                                                                         z.DateStart.Year == now.Year).ToList();
            var matches = new List<Match>();
            foreach (var item in tours)
            {
                var tourMatches = _dataContext.GetSet<Match>().Where(z => z.TournamentItemId == item.Id).ToList();
                matches.AddRange(tourMatches);
            }

            return matches;
        }

        public List<Match> GetNotTodayMatches()
        {
            var now = DateTime.Now;
            var tours = _dataContext.GetSet<TournamentItem>().Where(z => z.DateStart < now &&
                                                                         z.DateStart.Day != now.Day)
                .OrderByDescending(z => z.DateStart)
                .ToList();

            var matches = new List<Match>();
            foreach (var item in tours)
            {
                var tourMatches = _dataContext.GetSet<Match>().Where(z => z.TournamentItemId == item.Id).ToList();
                matches.AddRange(tourMatches);
            }

            return matches;
        }

        public List<Match> GetMatchesByTourItemId(Guid id)
        {
            return _dataContext.GetSet<Match>().Where(z => z.TournamentItemId == id).ToList();
        } 
    }
}
