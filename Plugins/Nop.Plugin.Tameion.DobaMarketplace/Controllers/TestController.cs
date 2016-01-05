using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.DobaMarketplace.Controllers
{
    public class TestController : BasePluginController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
