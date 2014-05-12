using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Antlr.Runtime;
using DomainModel;
using DomainModel.Entities;
using DomainModel.Repositories;
using Infrastructure;
using manager.Components;
using manager.Models;
using Microsoft.Ajax.Utilities;

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
        private readonly ISkillRepository _skillRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public PlayerController(IPlayerRepository playerRepository, IPositionRepository positionRepository,
            IEntityFactory entityFactory, ICountryRepository countryRepository, IUserRepository userRepository,
            IAuthenticationProvider authenticationProvider, IIllnessRepository illnessRepository,
            INumberingRepository numberingRepository, ISkillRepository skillRepository, ITeamRepository teamRepository,
            IEquipmentRepository equipmentRepository)
        {
            _playerRepository = playerRepository;
            _positionRepository = positionRepository;
            _entityFactory = entityFactory;
            _countryRepository = countryRepository;
            _userRepository = userRepository;
            _authenticationProvider = authenticationProvider;
            _illnessRepository = illnessRepository;
            _numberingRepository = numberingRepository;
            _skillRepository = skillRepository;
            _teamRepository = teamRepository;
            _equipmentRepository = equipmentRepository;
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

            var position = _positionRepository.Get(model.PositionId);

            var player = _entityFactory.Player(model.Name, model.Surname, 18, model.Weight, model.Growth, 0, 0, 100000, 5, 100, user,
                position, _illnessRepository.GetIllnessByName("healthy"), _countryRepository.Get(model.CountryId),
                String.Format("{0:D" + Constants.MaxLengthOfPublicId + "}", nextNumber),
                DateTime.Now);

            _playerRepository.Add(player);
            CreateSkillsForPlayer(player, position);
            return JsonSuccess();
        }

        private void CreateSkillsForPlayer(Player player, Position position)
        {
            var FW = new[] { 1, 2, 3, 4, 5, 7, 8, 13, 14 };
            var DEF = new[] { 1, 2, 3, 4, 5, 6, 8, 13, 14 };
            var GK = new[] { 8, 9, 10, 11, 12, 13, 14 };
            var MID = new[] { 1, 2, 3, 4, 5, 6, 8, 13, 14 };
            switch (position.PublicId)
            {
                case "FW":
                    CreateSkills(FW, player);
                    break;
                case "DEF":
                    CreateSkills(DEF, player);
                    break;
                case "GK":
                    CreateSkills(GK, player);
                    break;
                case "MID":
                    CreateSkills(MID, player);
                    break;
            }
        }

        private void CreateSkills(int[] arr, Player player)
        {
            foreach (var i in arr)
            {
                var skill = _entityFactory.SkillsPlayer(_skillRepository.GetSkillByOrdering(i), player);
                player.SkillPlayerCollection.Add(skill);
            }
        }

        [HttpPost]
        [Route("api/player/getplayerinfo")]
        public ActionResult GetPlayerInfo(string publicId)
        {
            var player = _playerRepository.GetPlayerByPublicId(publicId);
            if (player == null)
            {
                return JsonError("Player does not exists");
            }

            return JsonSuccess(new
            {
                Id = player.Id,
                Name = player.Name,
                Surname = player.Surname,
                Age = player.Age,
                Weight = player.Weight,
                Growth = player.Growth,
                Number = player.Number,
                Salary = player.Salary,
                Money = player.Money,
                Humor = player.Humor,
                Condition = player.Condition,
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
                CreateDate = player.CreateDate,
                TeamId = player.TeamId,
            });
        }

        [HttpPost]
        [Route("api/player/getplayersbyposition")]
        public ActionResult GetPlayersByPositions(string position)
        {
            ICollection<Player> players = new Collection<Player>();
            if (position == null)
            {
                players = _playerRepository.GetCollection();
            }
            else
            {
                players = _playerRepository.GetCollection(p => p.Position.PublicId == position);
            }
            if (players.Count == 0)
            {
                return JsonError("Players not found");
            }
            return JsonSuccess(players.Select(player => new
            {
                fullName = player.Name + " " + player.Surname,
                countryId = player.Country.PublicId,
                countryName = player.Country.Name,
                skillValue = player.SkillPlayerCollection.Sum(s => s.Value),
                publicId = player.PublicId
            }));
        }

        [HttpPost]
        [Route("api/equipment/getbytype")]
        public ActionResult GetEquipmentsByType(EquipmentType type)
        {
            var equipments = _equipmentRepository.GetEquipmentsByType(type);
            if (equipments.Count == 0)
            {
                return JsonError("Equipments not found");
            }
            else
            {
                return JsonSuccess(equipments.Select(e => new
                {
                    id = e.Id,
                    name = e.Name,
                    price = e.Price,
                    countOfMatch = e.CountOfMatch,
                    amountOfSkills = e.AmountOfSkills,
                    type = e.Type,
                    weatherType = e.WeatherType,
                    index = e.Index
                }));
            }
        }

        [HttpPost]
        [Route("api/order")]
        public ActionResult Order(ICollection<OrderModel> orders)
        {
            return JsonError("error");
        }

        [HttpPost]
        [Route("api/player/iscoatch")]
        public ActionResult IsCoatch()
        {
            var user = _userRepository.GetUserByUserName(GetCurrentUsername());
            Team coach = null;
            if (user != null)
            {
                coach = _teamRepository.GetCollection().FirstOrDefault(t => t.CoachId == user.Id);
            }
            return coach == null ? JsonError("Coach not found") : JsonSuccess(true);
        }

        [HttpPost]
        [Route("api/player/updateplayernumber")]
        public ActionResult UpdatePlayerNumber(Guid id, int number)
        {
            var player = _playerRepository.Get(id);
            if (player == null)
            {
                return JsonError("Player not found");
            }
            player.Number = number;
            return JsonSuccess(true);
        }

        [HttpPost]
        [Route("api/player/updateplayersalary")]
        public ActionResult UpdatePlayerSalary(Guid id, int salary)
        {
            var player = _playerRepository.Get(id);
            if (player == null)
            {
                return JsonError("Player not found");
            }
            player.Salary = salary;
            return JsonSuccess(true);
        }
    }
}