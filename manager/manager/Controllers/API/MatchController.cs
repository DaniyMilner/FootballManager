using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Entities;
using DomainModel.Repositories;
using manager.Components;

namespace manager.Controllers.API
{
    public class MatchController : DefaultController
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IEventLineRepository _eventLineRepository;
        public MatchController(IMatchRepository matchRepository, ITeamRepository teamRepository, 
                                IEventLineRepository eventLineRepository)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _eventLineRepository = eventLineRepository;
        }

        [HttpPost]
        [Route("api/match/getmatchresult")]
        public ActionResult GetMatchResultById(string id)
        {
            id = id.Replace("=", "").Replace("/", "");

            var match = _matchRepository.GetMatchByPublicId(id);
            if (match != null)
            {
                var homeTeam = _teamRepository.Get(match.HomeTeamId);
                var guestTeam = _teamRepository.Get(match.GuestTeamId);
                var eventLine = _eventLineRepository.GetEventsListByLineId(match.EventLineId).OrderByDescending(z=>z.Minute).ToList();
                return JsonSuccess();
            }
            return JsonError("no_team");
        }
    }
}