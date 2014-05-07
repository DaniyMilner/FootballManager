using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Repositories;
using manager.Components;

namespace manager.Controllers.API
{
    public class MenuController : DefaultController
    {
        private readonly ISeasonsRepository _seasonsRepository;

        public MenuController(ISeasonsRepository seasonsRepository)
        {
            _seasonsRepository = seasonsRepository;
        }

        [HttpPost]
        [Route("api/menu/getseasonslist")]
        public ActionResult GetSeasonsList()
        {
            var seasons = _seasonsRepository.GetCollection();
            var result = new List<object>();
            foreach (var seasonse in seasons)
            {
                result.Add(new
                {
                    id = seasonse.Id,
                    title = seasonse.Title
                });
            }
            return JsonSuccess(result);
        }
    }
}