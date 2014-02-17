using System.Web.Mvc;
using DomainModel.Repositories;
using manager.Components;

namespace manager.Controllers
{
    public class HomeController : DefaultController
    {

        public ActionResult Index()
        {
            return View();
        }

    }
}