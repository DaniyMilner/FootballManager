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
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationProvider _authenticationProvider;

        public PlayerController(IPlayerRepository playerRepository, IPositionRepository positionRepository,
            IEntityFactory entityFactory,ICountryRepository countryRepository, IUserRepository userRepository,
            IAuthenticationProvider authenticationProvider)
        {
            _playerRepository = playerRepository;
            _positionRepository = positionRepository;
            _entityFactory = entityFactory;
            _countryRepository = countryRepository;
            _userRepository = userRepository;
            _authenticationProvider = authenticationProvider;
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
            if (model == null)
            {
                return JsonError("model can not be null");
            }

            var user = _userRepository.GetUserByUserName(GetCurrentUsername());
            if (user == null)
            {
                return JsonError("User can not be null");
            }

            //var player = _entityFactory.Player(model.Name, model.Surname, 18, model.Weight,model.Growth,0,0,100000,5,100,)

                /*string name, string surname, int age, int weight, int growth, int number, int salary,
            int money, int humor, int condition, User user, Position position, Illness illness, Country country,
            string publicId,
            DateTime createDate*/

            //доделать когда будет регистрация
            return JsonSuccess();
        }
    }
}