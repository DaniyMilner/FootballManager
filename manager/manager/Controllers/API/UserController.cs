using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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

        public UserController(IUserRepository userRepository, IAuthenticationProvider authenticationProvider)
        {
            _userRepository = userRepository;
            _authenticationProvider = authenticationProvider;
        }

        [HttpPost]
        [Route("api/user/signup")]
        public ActionResult SignUp(UserSignUpModel userSignUpModel)
        {
            return JsonSuccess();
        }

        [HttpPost]
        [Route("api/user/signin")]
        public ActionResult SignIn()
        {
            return JsonSuccess();
        }

        [HttpPost]
        [Route("api/user/signout")]
        public ActionResult SignOut()
        {
            return JsonSuccess();
        }

        [HttpPost]
        [Route("api/user/getuserinfo")]
        public ActionResult GetUserInfo()
        {
            if (!_authenticationProvider.IsUserAuthenticated()) return JsonSuccess();
            return  JsonSuccess(_userRepository.GetUserByUserName(GetCurrentUsername()));
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