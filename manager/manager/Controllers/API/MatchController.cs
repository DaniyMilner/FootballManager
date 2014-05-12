using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DomainModel;
using DomainModel.Entities;
using DomainModel.Repositories;
using Infrastructure;
using manager.Components;
using manager.Models;
using Newtonsoft.Json;

namespace manager.Controllers.API
{
    public class MatchController : DefaultController
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IEventLineRepository _eventLineRepository;
        private readonly ITeamSettingsRepository _teamSettingsRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITournamentItemRepository _tournamentItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IArrangementRepository _arrangementRepository;
        private readonly IEntityFactory _entityFactory;
        private readonly IPlayerSettingsRepository _playerSettingsRepository;
        public MatchController(IMatchRepository matchRepository, ITeamRepository teamRepository,
                                IEventLineRepository eventLineRepository, ITeamSettingsRepository teamSettingsRepository,
                                IPlayerRepository playerRepository, IArrangementRepository arrangementRepository,
                                ITournamentItemRepository tournamentItemRepository, IUserRepository userRepository,
                                IEntityFactory entityFactory, IPlayerSettingsRepository playerSettingsRepository)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _eventLineRepository = eventLineRepository;
            _teamSettingsRepository = teamSettingsRepository;
            _playerRepository = playerRepository;
            _tournamentItemRepository = tournamentItemRepository;
            _userRepository = userRepository;
            _arrangementRepository = arrangementRepository;
            _entityFactory = entityFactory;
            _playerSettingsRepository = playerSettingsRepository;
        }

        [HttpPost]
        [Route("api/match/getmatchresult")]
        public ActionResult GetMatchResultById(string id)
        {
            id = StringHelper.CheckSymbols(id);
            var json = new JavaScriptSerializer();
            var match = _matchRepository.GetMatchByPublicId(id);
            if (match == null) return JsonError("no_team");

            var tour = _tournamentItemRepository.Get(match.TournamentItemId);
            var homeTeam = _teamRepository.Get(match.HomeTeamId);
            var guestTeam = _teamRepository.Get(match.GuestTeamId);
            var homeTeamInformation = GetCustomTeamInfo(homeTeam);
            var guestTeamInformation = GetCustomTeamInfo(guestTeam);
            if (String.IsNullOrEmpty(match.Result) || tour.DateStart > DateTime.Now) return JsonSuccess(new
              {
                  matchStart = false,
                  dateStart = tour.DateStart.ToUniversalTime(),
                  matchInfo = new
                  {
                      weatherName = match.Weather.Name,
                      weatherType = match.Weather.Type,
                      ticketPrice = match.TicketPrice,
                      dateStart = tour.DateStart.ToUniversalTime(),
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
                countShow = tour.DateStart < DateTime.Now ?
                                Convert.ToInt32(Math.Round((DateTime.Now - tour.DateStart).TotalMinutes)) + 1 : 0,
                reverse = (DateTime.Now - tour.DateStart).TotalMinutes > 45,
                matchInfo = new
                {
                    weatherName = match.Weather.Name,
                    weatherType = match.Weather.Type,
                    ticketPrice = match.TicketPrice,
                    dateStart = tour.DateStart.ToUniversalTime(),
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


        [HttpPost]
        [Route("api/team/getTeamForMatch")]
        public ActionResult GetTeamForMatch(string userId)
        {
            userId = StringHelper.CheckSymbols(userId);
            Guid id;
            Guid.TryParse(userId, out id);
            if (id == Guid.Empty) return JsonSuccess("error user id");

            var user = _userRepository.Get(id);
            if (user == null) return JsonSuccess("no user");

            var allTeams = _teamRepository.GetCollection();
            var team = allTeams.FirstOrDefault(z => z.CoachId == user.Id || z.AssistantId == user.Id);
            if (team == null) return JsonSuccess("user not coach");

            var teamPlayers = _playerRepository.GetAllPlayersByTeamId(team.Id);
            var customTeamPlayersList = CustomizeTeamPlayers(teamPlayers);

            var allTeamMatches = _matchRepository.GetAllTeamMatches(team.Id);
            if (allTeamMatches.Count == 0) return JsonSuccess("no matches for team");

            Match futureMatch = null;
            TournamentItem tourItem = null;

            foreach (var match in allTeamMatches)
            {
                var currentTourItem = _tournamentItemRepository.Get(match.TournamentItemId);
                if (futureMatch == null && tourItem == null)
                {
                    if (currentTourItem != null && currentTourItem.DateStart > DateTime.Now)
                    {
                        futureMatch = match;
                        tourItem = currentTourItem;
                    }
                }
                else
                {
                    if (currentTourItem != null && currentTourItem.DateStart > DateTime.Now &&
                        currentTourItem.DateStart < tourItem.DateStart)
                    {
                        futureMatch = match;
                        tourItem = currentTourItem;
                    }
                }
            }
            if (futureMatch == null || tourItem == null) return JsonSuccess("no match or tour");

            var teamSettings = _teamSettingsRepository.GetTeamSettingsByMatchAndTeamId(futureMatch.Id, team.Id);
            var customTeamSettings = CustomizeTeamSettings(teamSettings);

            var allArragements = _arrangementRepository.GetCollection().ToList();
            var customArragements = CustomizeArragementsList(allArragements);
            return JsonSuccess(new
            {
                teamId = team.Id,
                teamName = team.Name,
                teamShortName = team.ShortName,
                playersList = customTeamPlayersList,
                teamSettings = customTeamSettings,
                allArragements = customArragements,
                match = new
                {
                    id = futureMatch.Id,
                    date = tourItem.DateStart,
                    rivalName = futureMatch.HomeTeamId == team.Id ? _teamRepository.Get(futureMatch.GuestTeamId).Name : _teamRepository.Get(futureMatch.HomeTeamId).Name,
                    rivalShortName = futureMatch.HomeTeamId == team.Id ? _teamRepository.Get(futureMatch.GuestTeamId).ShortName : _teamRepository.Get(futureMatch.HomeTeamId).ShortName
                }
            });
        }

        [HttpPost]
        [Route("api/team/sendSettings")]
        public ActionResult SendSettings(TeamSettingsModel model)
        {
            var json = new JavaScriptSerializer();

            var match = _matchRepository.Get(model.MatchId);
            var arragement = _arrangementRepository.Get(model.ArragementId);
            var team = _teamRepository.Get(model.TeamId);

            bool isNew = false;
            var settings = _teamSettingsRepository.GetTeamSettingsByMatchAndTeamId(match.Id, team.Id);
            if (settings == null)
            {
                isNew = true;
                settings = _entityFactory.TeamSettings(match, arragement, team);
            }
            else
            {
                settings.SetAttagement(arragement);
            }
            settings.SetSettings(json.Serialize(new CustomTeamSettings
            {
                Corner = model.Corner,
                Freekick = model.FreeKick,
                Penalty = model.Penalty
            }));

            settings.SetLineUp(json.Serialize(new CustomLineUp
            {
                One = model.One,
                Two = model.Two,
                Three = model.Three,
                Four = model.Four,
                Five = model.Five,
                Six = model.Six,
                Seven = model.Seven,
                Eight = model.Eight,
                Nine = model.Nine,
                Ten = model.Ten,
                Eleven = model.Eleven
            }));

            settings.SetPlayerSend(model.PlayerId);

            var playerSettings = _playerSettingsRepository.GetPlayerSettingsByPlayerId(model.Capitan);
            if (playerSettings!=null)
            playerSettings.SetCaptain();

            if (isNew)
                _teamSettingsRepository.Add(settings);

            CreatePlayerSettings(model);

            return JsonSuccess();
        }

        private void CreatePlayerSettings(TeamSettingsModel model)
        {
            var json = new JavaScriptSerializer();
            var list = _playerSettingsRepository.GetPlayersSettingsByMatchId(model.MatchId);
            var playersIds = new List<Guid>
            {
                model.One,
                model.Two,
                model.Three,
                model.Four,
                model.Five,
                model.Six,
                model.Seven,
                model.Eight,
                model.Nine,
                model.Ten,
                model.Eleven
            };
            for (int i = 0; i < playersIds.Count; i++)
            {
                var id = playersIds[i];
                var item = list.FirstOrDefault(z => z.Player.Id == id);
                if (item != null)
                {
                    item.SetIndexField(i + 1);
                    if (id == model.Capitan)
                    {
                        item.SetCaptain();
                    }
                }
                else
                {
                    var player = _playerRepository.Get(id);
                    var match = _matchRepository.Get(model.MatchId);
                    var itemToCreate = _entityFactory.PlayerSettings(player, match, i + 1);
                    
                    var customSettings = new CustomPlayerSettings();
                    if (player.Position.PublicId != "GK")
                    {
                        customSettings.Orient = PlayerSettingsConstants.Orient;
                        customSettings.Pas = PlayerSettingsConstants.Pas;
                        customSettings.Strike = PlayerSettingsConstants.Strike;
                        customSettings.Oneone = PlayerSettingsConstants.OneoneField;
                        customSettings.Canopy = PlayerSettingsConstants.Canopy;
                        customSettings.Selection = PlayerSettingsConstants.Selection;
                        customSettings.Dedication = PlayerSettingsConstants.Dedication;
                        customSettings.Penalty = PlayerSettingsConstants.Penalty;
                    }
                    else
                    {
                        customSettings.Oneone = PlayerSettingsConstants.OneoneGk;
                        customSettings.Penalty = PlayerSettingsConstants.Penalty;
                        customSettings.Dedication = PlayerSettingsConstants.Dedication;
                        customSettings.Canopy = PlayerSettingsConstants.Canopy;
                    }
                    if (player.Id == model.Capitan)
                    {
                        itemToCreate.SetCaptain();
                    }
                    itemToCreate.SetSettings(json.Serialize(customSettings));

                    _playerSettingsRepository.Add(itemToCreate);
                }
            }
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

        private List<object> CustomizeTeamPlayers(IEnumerable<Player> teamPlayers)
        {
            var result = new List<object>();
            foreach (var player in teamPlayers)
            {
                result.Add(new
                {
                    id = player.Id,
                    name = player.Name,
                    surname = player.Surname,
                    publicId = player.PublicId,
                    position = player.Position.PublicId
                });
            }
            return result;
        }

        private object CustomizeTeamSettings(TeamSettings teamSettings)
        {
            if (teamSettings == null) return null;

            var json = new JavaScriptSerializer();
            var settings = json.Deserialize<CustomTeamSettings>(teamSettings.Settings);
            var lineUp = json.Deserialize<CustomLineUp>(teamSettings.LineUp);
            return new
            {
                settings = new
                {
                    cornerId = settings.Corner,
                    freekickId = settings.Freekick,
                    penaltyId = settings.Penalty
                },
                lineUp = new
                {
                    one = lineUp.One,
                    two = lineUp.Two,
                    three = lineUp.Three,
                    four = lineUp.Four,
                    five = lineUp.Five,
                    six = lineUp.Six,
                    seven = lineUp.Seven,
                    eight = lineUp.Eight,
                    nine = lineUp.Nine,
                    ten = lineUp.Ten,
                    eleven = lineUp.Eleven
                },
                arragement = new
                {
                    id = teamSettings.Arrangement.Id,
                    scheme = teamSettings.Arrangement.Scheme,
                    type = teamSettings.Arrangement.Type
                }
            };
        }

        private List<object> CustomizeArragementsList(IEnumerable<Arrangement> list)
        {
            var result = new List<object>();
            foreach (var item in list)
            {
                result.Add(new
                {
                    id = item.Id,
                    scheme = item.Scheme,
                    type = item.Type
                });
            }

            return result;
        }
    }
}