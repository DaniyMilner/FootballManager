﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace manager.Components.ActionResults
{
    public class BadRequestResult : HttpStatusCodeResult
    {
        public BadRequestResult()
            : base(HttpStatusCode.BadRequest)
        {

        }
    }
}