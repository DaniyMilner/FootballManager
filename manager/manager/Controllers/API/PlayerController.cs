using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Entities;
using DomainModel.Repositories;
using manager.Components;
using manager.Models;

namespace manager.Controllers.API
{
    public class PlayerController : DefaultController
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IEntityFactory _entityFactory;
        private readonly ICountryRepository _countryRepository;

        public PlayerController(IPlayerRepository playerRepository, IPositionRepository positionRepository,
            IEntityFactory entityFactory,ICountryRepository countryRepository)
        {
            _playerRepository = playerRepository;
            _positionRepository = positionRepository;
            _entityFactory = entityFactory;
            _countryRepository = countryRepository;
        }

        [HttpPost]
        [Route("api/player/getallpositions")]
        public ActionResult GetAllPositions()
        {
            return JsonSuccess(_positionRepository.GetCollection());
        }

        [HttpPost]
        [Route("api/player/getallcountries")]
        public ActionResult GetAllCountries()
        {
            return JsonSuccess(_countryRepository.GetCollection());
        }

        [HttpPost]
        [Route("api/player/createplayer")]
        public ActionResult CreatePlayer(PlayerCreateModel model)
        {
            //доделать когда будет регистрация
            return JsonSuccess();
        }
    }
}