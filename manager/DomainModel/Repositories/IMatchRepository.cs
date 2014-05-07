using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface IMatchRepository : IRepository<Match>
    {
        List<Match> GetAllNotGeneratedMatches();
        Match GetMatchByPublicId(string publicId);
        List<Match> GetTodayMatches();
        List<Match> GetNotTodayMatches();
        List<Match> GetMatchesByTourItemId(Guid id);
    }
}
