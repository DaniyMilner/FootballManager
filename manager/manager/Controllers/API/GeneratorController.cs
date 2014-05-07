using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Generator;
using DomainModel;
using DomainModel.Repositories;
using manager.Components;

namespace manager.Controllers.API
{
    public class GeneratorController : DefaultController
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IEntityFactory _entityFactory;
        private readonly ITeamRepository _teamRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITeamSettingsRepository _teamSettingsRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IArrangementRepository _arrangementRepository;
        private readonly IPlayerSettingsRepository _playerSettingsRepository;
        private readonly IEventLineRepository _eventLineRepository;
        private readonly ITournamentItemRepository _tournamentItemRepository;

        public GeneratorController(IMatchRepository matchRepository,
                                    IEntityFactory entityFactory,
                                    ITeamRepository teamRepository,
                                    ICountryRepository countryRepository,
                                    ITeamSettingsRepository teamSettingsRepository,
                                    IPlayerRepository playerRepository,
                                    IArrangementRepository arrangementRepository,
                                    IPlayerSettingsRepository playerSettingsRepository,
                                    IEventLineRepository eventLineRepository,
                                    ITournamentItemRepository tournamentItemRepository)
        {
            _entityFactory = entityFactory;
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _countryRepository = countryRepository;
            _teamSettingsRepository = teamSettingsRepository;
            _playerRepository = playerRepository;
            _arrangementRepository = arrangementRepository;
            _playerSettingsRepository = playerSettingsRepository;
            _eventLineRepository = eventLineRepository;
            _tournamentItemRepository = tournamentItemRepository;
        }

        [HttpPost]
        [Route("api/generator/run")]
        public ActionResult Run()
        {
            var generator = new Generator();
            var time = generator.Run(_matchRepository, _entityFactory, _teamRepository, _countryRepository, _teamSettingsRepository,
                _playerRepository, _arrangementRepository, _playerSettingsRepository, _eventLineRepository);
            return JsonSuccess(new { date = time });
        }

        [HttpPost]
        [Route("api/generator/getTodayMatches")]
        public ActionResult GetTodayMatches()
        {
            var matches = _matchRepository.GetTodayMatches();
            if (matches.Count == 0) return JsonSuccess("no_matches");

            var result = new List<object>();
            foreach (var match in matches)
            {
                var tour = _tournamentItemRepository.Get(match.TournamentItemId);
                var home = _teamRepository.Get(match.HomeTeamId);
                var guest = _teamRepository.Get(match.GuestTeamId);
                result.Add(new
                {
                    id = match.Id,
                    homeName = home.Name,
                    guestName = guest.Name,
                    dateStart = tour.DateStart,
                    isGenerated = !String.IsNullOrEmpty(match.Result),
                    homeGoal = match.HomeGoal,
                    guestGoal = match.GuestGoal,
                    publicId = match.PublicId,
                    homeShortName = home.ShortName,
                    guestShortName = guest.ShortName
                });
            }
            return JsonSuccess(result);
        }

        [HttpPost]
        [Route("api/generator/getNotTodayMatches")]
        public ActionResult GetNotTodayMatches()
        {
            var matches = _matchRepository.GetNotTodayMatches();
            if (matches.Count == 0) return JsonSuccess("no_matches");

            var result = new List<object>();
            foreach (var match in matches)
            {
                var tour = _tournamentItemRepository.Get(match.TournamentItemId);
                var home = _teamRepository.Get(match.HomeTeamId);
                var guest = _teamRepository.Get(match.GuestTeamId);
                result.Add(new
                {
                    id = match.Id,
                    homeName = home.Name,
                    guestName = guest.Name,
                    dateStart = tour.DateStart,
                    isGenerated = !String.IsNullOrEmpty(match.Result),
                    homeGoal = match.HomeGoal,
                    guestGoal = match.GuestGoal,
                    publicId = match.PublicId,
                    homeShortName = home.ShortName,
                    guestShortName = guest.ShortName
                });
            }
            return JsonSuccess(result);
        }
    }
}