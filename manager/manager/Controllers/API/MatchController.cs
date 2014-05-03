using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
        private readonly ITeamSettingsRepository _teamSettingsRepository;
        private readonly IArrangementRepository _arrangementRepository;
        private readonly IPlayerRepository _playerRepository;
        public MatchController(IMatchRepository matchRepository, ITeamRepository teamRepository,
                                IEventLineRepository eventLineRepository, ITeamSettingsRepository teamSettingsRepository,
                                IArrangementRepository arrangementRepository, IPlayerRepository playerRepository)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _eventLineRepository = eventLineRepository;
            _teamSettingsRepository = teamSettingsRepository;
            _arrangementRepository = arrangementRepository;
            _playerRepository = playerRepository;
        }

        [HttpPost]
        [Route("api/match/getmatchresult")]
        public ActionResult GetMatchResultById(string id)
        {
            id = CheckSymbols(id);
            var json = new JavaScriptSerializer();
            var match = _matchRepository.GetMatchByPublicId(id);
            if (match == null) return JsonError("no_team");

            var homeTeam = _teamRepository.Get(match.HomeTeamId);
            var guestTeam = _teamRepository.Get(match.GuestTeamId);
            var homeTeamInformation = GetCustomTeamInfo(homeTeam);
            var guestTeamInformation = GetCustomTeamInfo(guestTeam);
            if (String.IsNullOrEmpty(match.Result) || match.DateStart > DateTime.Now) return JsonSuccess(new
              {
                  matchStart = false,
                  dateStart = match.DateStart.ToUniversalTime(),
                  matchInfo = new
                  {
                      weatherName = match.Weather.Name,
                      weatherType = match.Weather.Type,
                      ticketPrice = match.TicketPrice,
                      dateStart = match.DateStart.ToUniversalTime(),
                      fans = match.FansCount
                  },
                  homeTeamInfo = homeTeamInformation,
                  guestTeamInfo = guestTeamInformation
              });

            var eventLine = _eventLineRepository.GetEventsListByLineId(match.EventLineId).OrderBy(z => z.Minute).ToList();

            var homeSettings = _teamSettingsRepository.GetTeamSettingsByMatchAndTeamId(match.Id, homeTeam.Id);
            var guestSettings = _teamSettingsRepository.GetTeamSettingsByMatchAndTeamId(match.Id, guestTeam.Id);

            var homePlayers = _playerRepository.GetCollectionByLineUp(json.Deserialize<CustomLineUp>(homeSettings.LineUp));
            var guestPlayers = _playerRepository.GetCollectionByLineUp(json.Deserialize<CustomLineUp>(guestSettings.LineUp));

            var result = json.Deserialize<List<MatchEvent>>(match.Result);

            /*---------------------------------------------*/
            var resultHomePlayers = GetCustomTeamPlayers(homePlayers);
            var resultGuestPlayers = GetCustomTeamPlayers(guestPlayers);
            var events = GetCustomEventLines(eventLine);

            return JsonSuccess(new
            {
                matchStart = true,
                dateStart = match.DateStart.ToUniversalTime(),
                countShow = match.DateStart < DateTime.Now ? 
                                Convert.ToInt32(Math.Round((DateTime.Now - match.DateStart).TotalMinutes)) : 0,
                reverse = (DateTime.Now - match.DateStart).Hours > 0,
                matchInfo = new
                {
                    weatherName = match.Weather.Name,
                    weatherType = match.Weather.Type,
                    ticketPrice = match.TicketPrice,
                    dateStart = match.DateStart.ToUniversalTime(),
                    fans = match.FansCount
                },
                matchResult = result,
                homePlayers = resultHomePlayers,
                guestPlayers = resultGuestPlayers,
                homeArragement = new
                {
                    scheme = homeSettings.Arrangement.Scheme,
                    type = homeSettings.Arrangement.Type
                },
                guestArragement = new
                {
                    scheme = guestSettings.Arrangement.Scheme,
                    type = guestSettings.Arrangement.Type
                },
                eventsLine = events,
                homeTeamInfo = homeTeamInformation,
                guestTeamInfo = guestTeamInformation
            });
        }

        private List<object> GetCustomTeamPlayers(IEnumerable<Player> teamPlayers)
        {
            var resultPlayers = new List<object>();
            foreach (var teamPlayer in teamPlayers)
            {
                resultPlayers.Add(new
                {
                    id = teamPlayer.Id,
                    name = teamPlayer.Name,
                    surname = teamPlayer.Surname,
                    number = teamPlayer.Number,
                    humor = teamPlayer.Humor,
                    condition = teamPlayer.Condition,
                    publicId = teamPlayer.PublicId,
                    countryName = teamPlayer.Country.Name,
                    countryPublicId = teamPlayer.Country.PublicId,
                    illnessName = teamPlayer.Illness.IllnessName,
                    positionName = teamPlayer.Position.Name,
                    positionPublicId = teamPlayer.Position.PublicId,
                });
            }
            return resultPlayers;
        }

        private List<object> GetCustomEventLines(IEnumerable<EventLine> eventLines)
        {
            var events = new List<object>();
            foreach (var line in eventLines)
            {
                events.Add(new
                {
                    minute = line.Minute,
                    type = line.Type,
                    playerId = line.Player.Id
                });
            }
            return events;
        }

        private object GetCustomTeamInfo(Team team)
        {
            return new
            {
                id = team.Id,
                name = team.Name,
                shortName = team.ShortName,
                stadium = team.Stadium,
                country = new
                {
                    name = team.Country.Name,
                    publicId = team.Country.PublicId
                }
            };
        }

        private string CheckSymbols(string id)
        {
            return id.Replace("=", "").Replace("/", "");
        }
    }
}