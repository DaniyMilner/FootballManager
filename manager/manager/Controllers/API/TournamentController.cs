using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Entities;
using DomainModel.Repositories;
using Infrastructure;
using manager.Components;

namespace manager.Controllers.API
{
    public class TournamentController : DefaultController
    {
        private readonly ISeasonsRepository _seasonsRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentItemRepository _tournamentItemRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;

        public TournamentController(ISeasonsRepository seasonsRepository, ITournamentRepository tournamentRepository,
                                    ITournamentItemRepository tournamentItemRepository, IMatchRepository matchRepository,
                                    ITeamRepository teamRepository)
        {
            _seasonsRepository = seasonsRepository;
            _tournamentRepository = tournamentRepository;
            _tournamentItemRepository = tournamentItemRepository;
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
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

        [HttpPost]
        [Route("api/tournament/gettournamentinfo")]
        public ActionResult GetTournamentInfo(string id)
        {
            id = StringHelper.CheckSymbols(id);
            var tournament = _tournamentRepository.GeTournamentByPublicId(id);
            if (tournament == null) return JsonSuccess("no_tournament");

            var allTours = tournament.TournamentItemCollection.OrderBy(z => z.ItemNumber).ToList();
            if (allTours.Count == 0) return JsonSuccess("no_tours");

            var table = new List<TournamentTableItem>();

            var matchesFirstTour = _matchRepository.GetMatchesByTourItemId(allTours[0].Id);
            foreach (var match in matchesFirstTour)
            {
                var homeTeam = _teamRepository.Get(match.HomeTeamId);
                var guestTeam = _teamRepository.Get(match.GuestTeamId);

                table.Add(GetTableTeam(homeTeam));
                table.Add(GetTableTeam(guestTeam));
            }

            var playedTours = allTours.Where(z => z.DateStart < DateTime.Now).ToList();
            foreach (var item in playedTours)
            {
                var matches = _matchRepository.GetMatchesByTourItemId(item.Id);
                foreach (var match in matches)
                {
                    var homeTeam = table.First(z => z.TeamId == match.HomeTeamId);
                    var guestTeam = table.First(z => z.TeamId == match.GuestTeamId);
                    homeTeam.Goals += match.HomeGoal;
                    guestTeam.Goals += match.GuestGoal;
                    homeTeam.LostGoals += match.GuestGoal;
                    guestTeam.LostGoals += match.HomeGoal;
                    homeTeam.DifferenceGoals = homeTeam.Goals - homeTeam.LostGoals;
                    guestTeam.DifferenceGoals = guestTeam.Goals - guestTeam.LostGoals;
                    homeTeam.Games += 1;
                    guestTeam.Games += 1;
                    if (match.HomeGoal == match.GuestGoal)
                    {
                        homeTeam.Draw += 1;
                        guestTeam.Draw += 1;
                        homeTeam.Points += 1;
                        guestTeam.Points += 1;
                    }
                    else if (match.HomeGoal > match.GuestGoal)
                    {
                        homeTeam.Win += 1;
                        guestTeam.Lost += 1;
                        homeTeam.Points += 3;
                    }
                    else
                    {
                        guestTeam.Win += 1;
                        homeTeam.Lost += 1;
                        guestTeam.Points += 3;
                    }
                }
            }

            return JsonSuccess(new
            {
                tournamentName = tournament.Title,
                country = tournament.Country.PublicId,
                table = table.OrderByDescending(z => z.Points)
                     .ThenByDescending(z => z.DifferenceGoals)
                     .ThenByDescending(z => z.Goals).ToList()
            });
        }

        [HttpPost]
        [Route("api/tournament/getlastmatches")]
        public ActionResult GetLastMatches(string id)
        {
            id = StringHelper.CheckSymbols(id);
            var tournament = _tournamentRepository.GeTournamentByPublicId(id);
            if (tournament == null) return JsonSuccess("no_tournament");

            var allTours = tournament.TournamentItemCollection.OrderBy(z => z.ItemNumber).ToList();
            if (allTours.Count == 0) return JsonSuccess("no_tours");

            var tour = allTours.Where(z => z.DateStart < DateTime.Now).ToList();
            if (tour.Count==0) return JsonSuccess("no_last_matches");
            
            var tourItem = tour.OrderByDescending(z => z.DateStart).Take(1).First();

            var matches = _matchRepository.GetMatchesByTourItemId(tourItem.Id);
            var result = new List<object>();
            foreach (var item in matches)
            {
                var homeTeam = _teamRepository.Get(item.HomeTeamId);
                var guestTeam = _teamRepository.Get(item.GuestTeamId);
                result.Add(new
                {
                    matchPublicId = item.PublicId,
                    homeTeamName = homeTeam.Name,
                    homeTeamShortName = homeTeam.ShortName,
                    guestTeamName = guestTeam.Name,
                    guestTeamShortName = guestTeam.ShortName
                });
            }

            return JsonSuccess(new { resultList = result, tour = tourItem.ItemNumber });
        }

        [HttpPost]
        [Route("api/tournament/getnextmatches")]
        public ActionResult GetNextMatches(string id)
        {
            id = StringHelper.CheckSymbols(id);
            var tournament = _tournamentRepository.GeTournamentByPublicId(id);
            if (tournament == null) return JsonSuccess("no_tournament");

            var allTours = tournament.TournamentItemCollection.OrderBy(z => z.ItemNumber).ToList();
            if (allTours.Count == 0) return JsonSuccess("no_tours");

            var tours = allTours.Where(z => z.DateStart > DateTime.Now).ToList();
            if (tours.Count == 0) return JsonSuccess("no_tours_next");

            var tour = tours.OrderBy(z => z.DateStart).Take(1).First();

            var matches = _matchRepository.GetMatchesByTourItemId(tour.Id);
            var result = new List<object>();
            foreach (var item in matches)
            {
                var homeTeam = _teamRepository.Get(item.HomeTeamId);
                var guestTeam = _teamRepository.Get(item.GuestTeamId);
                result.Add(new
                {
                    matchPublicId = item.PublicId,
                    homeTeamName = homeTeam.Name,
                    homeTeamShortName = homeTeam.ShortName,
                    guestTeamName = guestTeam.Name,
                    guestTeamShortName = guestTeam.ShortName
                });
            }

            return JsonSuccess(new { resultList = result, tour = tour.ItemNumber });
        }

        private TournamentTableItem GetTableTeam(Team team)
        {
            return new TournamentTableItem(team.Id, team.Name, team.ShortName);
        }
    }
}