using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Entities;
using DomainModel.Repositories;
using manager.Components;
using Newtonsoft.Json;

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
            return JsonSuccess(players.Select(player => new
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
            }));
        }

        [HttpPost]
        [Route("api/team/applytojoin")]
        public ActionResult ApplyToJoin(Guid playerId)
        {
            return JsonSuccess();
        }
    }
}