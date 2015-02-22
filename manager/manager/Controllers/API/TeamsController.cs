using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using DomainModel.Entities;
using DomainModel.Repositories;
using manager.Components;

namespace manager.Controllers.API
{
    public class TeamsController : DefaultController
    {

        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICountryRepository _countryRepository;

        public TeamsController(ITeamRepository teamRepository, IPlayerRepository playerRepository, ICountryRepository countryRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _countryRepository = countryRepository;
        }

        [HttpPost]
        [Route("api/teams/getteamsbycountrypublicid/{publicId?}")]
        public ActionResult GetTeamsByCountryPublicId(string publicId)
        {
            ICollection<Team> teamCollection = new Collection<Team>();
            if (publicId == null)
            {
                teamCollection = _teamRepository.GetCollection();
            }
            else
            {
                var country = _countryRepository.GetCountryByPublicId(publicId);
                teamCollection = country.TeamCollection;
            }
            return JsonSuccess(teamCollection.Select(team => new
                {
                    Id = team.Id,
                    Name = team.Name,
                    ShortName = team.ShortName,
                    Country = team.Country.Name,
                    CountryShortName = team.Country.PublicId,
                    Stadium = team.Stadium,
                    Year = team.Year
                }));
        }

        [HttpPost]
        [Route("api/team/getteaminfobypublicid")]
        public ActionResult GetTeamInfo(string id)
        {
            var team = _teamRepository.GeTeamByPublicId(id);
            if (team == null)
            {
                return JsonError("Team not found");
            }
            var players = _playerRepository.GetPlayersByTeamId(team.Id);
            return JsonSuccess(GetPlayersForResponse(players));
        }

        [HttpPost]
        [Route("api/team/applytojoin")]
        public ActionResult ApplyToJoin(Guid playerId, Guid teamId)
        {
            var player = _playerRepository.Get(playerId);
            if (player == null)
                return JsonError("Player not found");
            var team = _teamRepository.Get(teamId);
            if (team == null)
                return JsonError("Team not found");

            player.TeamId = teamId;

            return JsonSuccess();
        }

        [HttpPost]
        [Route("api/team/getteamname")]
        public ActionResult GetTeamName(Guid id)
        {
            var team = _teamRepository.Get(id);
            if (team == null)
            {
                return JsonError("Team not found");
            }
            return JsonSuccess(new
            {
                name = team.Name,
                shortName = team.ShortName
            });
        }

        [HttpPost]
        [Route("api/team/getteamnamebypublicid")]
        public ActionResult GetTeamNameByPublicId(string id)
        {
            var team = _teamRepository.GeTeamByPublicId(id);
            if (team == null)
            {
                return JsonError("Team not found");
            }
            return JsonSuccess(new
            {
                name = team.Name,
                shortName = team.ShortName
            });
        }

        [HttpPost]
        [Route("api/team/getteambyid")]
        public ActionResult GetTeamById(Guid id)
        {
            var team = _teamRepository.Get(id);
            if (team == null)
            {
                return JsonError("Team not found");
            }
            var players = _playerRepository.GetAllPlayersByTeamId(team.Id);
            return JsonSuccess(GetPlayersForResponse(players));
        }

        [HttpPost]
        [Route("api/team/removeplayer")]
        public ActionResult RemovePlayer(Guid teamId, Guid playerId)
        {
            var player = _playerRepository.GetPlayersByTeamId(teamId).FirstOrDefault(p => p.Id == playerId);
            if (player != null)
            {
                player.TeamId = null;
                player.Salary = 0;
                player.Number = 0;
                return JsonSuccess(true);
            }
            return JsonError("Player not found");
        }

        private static object GetPlayersForResponse(IEnumerable<Player> players)
        {
            return players.Select(player => new
            {
                Id = player.Id,
                Name = player.Name,
                Surname = player.Surname,
                Position = new
                {
                    Id = player.Position.Id,
                    Name = player.Position.Name,
                    PublicId = player.Position.PublicId
                },
                Illness = new
                {
                    Id = player.Illness.Id,
                    IllnessName = player.Illness.IllnessName,
                    TimeForRecovery = player.Illness.TimeForRecovery
                },
                Country = new
                {
                    Id = player.Country.Id,
                    Name = player.Country.Name,
                    PublicId = player.Country.PublicId
                },
                Skills = player.SkillPlayerCollection.Select(skill => new
                {
                    id = skill.Skill.Ordering,
                    value = skill.Value
                }),
                PublicId = player.PublicId,
            });
        }
    }
}