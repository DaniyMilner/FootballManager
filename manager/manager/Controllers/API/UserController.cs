using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DomainModel;
using DomainModel.Entities;
using DomainModel.Repositories;
using manager.Components;
using manager.Models;
using NuGet;
using Constants = Infrastructure.Constants;

namespace manager.Controllers.API
{
    public class UserController : DefaultController
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly IEntityFactory _entityFactory;
        private readonly INumberingRepository _numberingRepository;

        public UserController(IUserRepository userRepository, IAuthenticationProvider authenticationProvider,
            IEntityFactory entityFactory, INumberingRepository numberingRepository)
        {
            _userRepository = userRepository;
            _authenticationProvider = authenticationProvider;
            _entityFactory = entityFactory;
            _numberingRepository = numberingRepository;
        }

        [HttpPost]
        [Route("api/user/signup")]
        public ActionResult SignUp(UserSignUpModel userSignUpModel)
        {
            if (userSignUpModel == null)
            {
                return JsonError("Model can't be null");
            }

            var nextValue = _numberingRepository.GetNextNumber(NumberingType.User);
            var nextNumber = nextValue.Number;
            nextValue.UpdateNumber(++nextNumber);

            var user = _entityFactory.User(userSignUpModel.Username, userSignUpModel.Password, userSignUpModel.Email,
                userSignUpModel.ParentId, userSignUpModel.Skype, userSignUpModel.Birthday, userSignUpModel.City,
                userSignUpModel.AboutMySelf, userSignUpModel.Sex,
                String.Format("{0:D" + Constants.MaxLengthOfPublicId + "}", nextNumber));
            _userRepository.Add(user);

            _authenticationProvider.SignIn(user.UserName, false);
            return JsonSuccess(true);
        }

        [HttpPost]
        [Route("api/user/signin")]
        public ActionResult SignIn(UserSignInModel userSignInModel)
        {
            if (userSignInModel == null)
            {
                return JsonError("Model can not be null");
            }

            var user = _userRepository.GetUserByUserName(userSignInModel.Username);
            if (user == null)
            {
                return JsonError("User does not exists");
            }

            if (!user.VerifyPassword(userSignInModel.Password))
            {
                return JsonError("User password not correct");
            }

            _authenticationProvider.SignIn(user.UserName, userSignInModel.RememberMe);

            return JsonSuccess(true);
        }

        [HttpPost]
        [Route("api/user/signout")]
        public ActionResult SignOut()
        {
            _authenticationProvider.SignOut();
            return JsonSuccess();
        }

        [HttpPost]
        [Route("api/user/getuserinfo")]
        public ActionResult GetUserInfo()
        {
            if (_authenticationProvider.IsUserAuthenticated())
            {
                var user = _userRepository.GetUserByUserName(GetCurrentUsername());
                var playersCollections = new List<object>();

                foreach (var player in user.PlayerCollection)
                {
                    playersCollections.Add(new
                    {
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
                        PublicId = player.PublicId,
                        CreateDate = player.CreateDate,
                        TeamId = player.TeamId,
                    });
                }

                return JsonSuccess(new
                {
                    User = new
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        Skype = user.Skype,
                        ParentId = user.ParentId,
                        Birthday = user.Birthday,
                        City = user.City,
                        AboutMySelf = user.AboutMySelf,
                        Sex = user.Sex,
                        PlayerCollection = playersCollections,
                        PublicId = user.PublicId
                    },
                    Language = _userRepository.GetUserByUserName(GetCurrentUsername()).Language
                });
            }
            return JsonSuccess(new
            {
                Language = Constants.DefaultLanguage
            });
        }

        [HttpPost]
        [Route("api/user/usernameexists")]
        public ActionResult UserNameExist(string username)
        {
            if (_userRepository.GetUserByUserName(username) == null) return JsonSuccess(true);
            return JsonError("This login allready exists");
        }

        [HttpPost]
        [Route("api/user/emailexists")]
        public ActionResult EmailExists(string email)
        {
            if (_userRepository.GetUserByEmail(email) == null) return JsonSuccess(true);
            return JsonError("This email allready exists");
        }

        [HttpPost]
        [Route("api/user/getuserlanguage")]
        public ActionResult GetUserLanguage()
        {
            if (_authenticationProvider.IsUserAuthenticated())
            {
                return JsonSuccess(new
                {
                    Language = _userRepository.GetUserByUserName(GetCurrentUsername()).Language
                });
            }
            return JsonSuccess(new
            {
                Language = Constants.DefaultLanguage
            });
        }

        [HttpPost]
        [Route("api/user/getuserinfobypublicId")]
        public ActionResult GetUserInfoByPublicId(string publicId)
        {
            if (String.IsNullOrEmpty(publicId))
            {
                return JsonError("Public id can not be empty");
            }

            var user = _userRepository.GetUserByPublicId(publicId);

            if (user == null)
            {
                return JsonError("User not found");
            }

            return JsonSuccess(new
            {
                User = new
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Skype = user.Skype,
                    ParentId = user.ParentId,
                    Birthday = user.Birthday,
                    City = user.City,
                    AboutMySelf = user.AboutMySelf,
                    Sex = user.Sex,
                    PublicId = user.PublicId
                }
            });
        }
    }
}