using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Generator;
using manager.Components;

namespace manager.Controllers.API
{
    public class GeneratorController : DefaultController
    {
        public GeneratorController()
        {
            
        }

        [HttpPost]
        [Route("api/generator/run")]
        public ActionResult Run()
        {
            Generator generator = new Generator();
            generator.Run();
            return JsonSuccess();
        }
    }
}