using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Entities;
using DomainModel.Repositories;
using Infrastructure;
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
        private readonly IIllnessRepository _illnessRepository;
        private readonly INumberingRepository _numberingRepository;

        public PlayerController(IPlayerRepository playerRepository, IPositionRepository positionRepository,
            IEntityFactory entityFactory,ICountryRepository countryRepository, IUserRepository userRepository,
            IAuthenticationProvider authenticationProvider, IIllnessRepository illnessRepository,
            INumberingRepository numberingRepository)
        {
            _playerRepository = playerRepository;
            _positionRepository = positionRepository;
            _entityFactory = entityFactory;
            _countryRepository = countryRepository;
            _userRepository = userRepository;
            _authenticationProvider = authenticationProvider;
            _illnessRepository = illnessRepository;
            _numberingRepository = numberingRepository;
        }

        [HttpPost]
        [Route("api/player/getallpositions")]
        public ActionResult GetAllPositions()
        {
            var positionsList = new List<Object>();

            foreach (var position in _positionRepository.GetCollection())
            {
                positionsList.Add(new 
                {
                    Id = position.Id,
                    Name = position.Name,
                    PublicId = position.PublicId
                });
            }
            return JsonSuccess(positionsList);
        }

        [HttpPost]
        [Route("api/player/getallcountries")]
        public ActionResult GetAllCountries()
        {
            var countriesList = new List<Object>();
            foreach (var country in _countryRepository.GetCollection())
            {
                countriesList.Add(new
                {
                    Id = country.Id,
                    Name = country.Name,
                    PublicId = country.PublicId
                });
            }
            return JsonSuccess(countriesList);
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

            var nextValue = _numberingRepository.GetNextNumber(NumberingType.Player);
            var nextNumber = nextValue.Number;
            nextValue.UpdateNumber(++nextNumber);

            var player = _entityFactory.Player(model.Name, model.Surname, 18, model.Weight, model.Growth, 0, 0, 100000, 5, 100, user,
                _positionRepository.Get(model.PositionId), _illnessRepository.GetIllnessByName("healthy"), _countryRepository.Get(model.CountryId),
                String.Format("{0:D" + Constants.MaxLengthOfPublicId + "}", nextNumber),
                DateTime.Now);

            _playerRepository.Add(player);
            return JsonSuccess();
        }
    }
}