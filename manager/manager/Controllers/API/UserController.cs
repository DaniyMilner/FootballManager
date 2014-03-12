using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DomainModel;
using DomainModel.Repositories;
using Infrastructure;
using manager.Components;
using manager.Models;

namespace manager.Controllers.API
{
    public class UserController : DefaultController
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly IEntityFactory _entityFactory;

        public UserController(IUserRepository userRepository, IAuthenticationProvider authenticationProvider,
            IEntityFactory entityFactory)
        {
            _userRepository = userRepository;
            _authenticationProvider = authenticationProvider;
            _entityFactory = entityFactory;
        }

        [HttpPost]
        [Route("api/user/signup")]
        public ActionResult SignUp(UserSignUpModel userSignUpModel)
        {
            if (userSignUpModel == null)
            {
                return JsonError("Model can't be null");
            }

            var user = _entityFactory.User(userSignUpModel.Username, userSignUpModel.Password, userSignUpModel.Email,
                userSignUpModel.ParentId, userSignUpModel.Skype, userSignUpModel.Birthday, userSignUpModel.City,
                userSignUpModel.AboutMySelf, userSignUpModel.Sex);
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
                return JsonSuccess(new
                {
                    User = _userRepository.GetUserByUserName(GetCurrentUsername()),
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
    }
}