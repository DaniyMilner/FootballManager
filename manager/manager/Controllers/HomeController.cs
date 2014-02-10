using System.Web.Mvc;
using DomainModel.Repositories;
using manager.Components;

namespace manager.Controllers
{
    public class HomeController : DefaultController
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}