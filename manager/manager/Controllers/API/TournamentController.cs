using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Repositories;
using Infrastructure;
using manager.Components;

namespace manager.Controllers.API
{
    public class TournamentController : DefaultController
    {
        private readonly ISeasonsRepository _seasonsRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentController(ISeasonsRepository seasonsRepository, ITournamentRepository tournamentRepository)
        {
            _seasonsRepository = seasonsRepository;
            _tournamentRepository = tournamentRepository;
        }

        [HttpPost]
        [Route("api/tournament/gettournamentlistbyseasonid")]
        public ActionResult GetTournamentList(string seasonTitle)
        {
            seasonTitle = StringHelper.CheckSymbols(seasonTitle);
            var season = _seasonsRepository.GetSeasonByTitle(seasonTitle);
            if (season == null) return JsonSuccess("no_season");
            
            var tournaments = _tournamentRepository.GeTournamentsBySeasonId(season.Id);
            var result = new List<object>();
            foreach (var tournament in tournaments)
            {
                result.Add(new
                {
                    id = tournament.Id,
                    title = tournament.Title,
                    country = tournament.Country.PublicId,
                    publicId = tournament.PublicId
                });
            }
            return JsonSuccess(result);
        }
    }
}