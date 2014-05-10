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
    }
}