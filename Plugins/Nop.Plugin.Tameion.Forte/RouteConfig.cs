using Nop.Plugin.Tameion.Forte.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.Forte
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Tameion.Forte.ForteController",
                "Admin/Forte/{action}/{id}",
                new { area = "Admin", controller = "Forte", action = "", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.Forte.Controllers" }
            );
            
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
