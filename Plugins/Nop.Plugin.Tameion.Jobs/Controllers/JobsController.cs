﻿using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.Jobs.Controllers
{
    public class JobsController : BasePluginController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
