using Nop.Plugin.Tameion.Cars.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.Cars
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            //var route = routes.MapRoute("Plugin.Tameion.Cars.CarsController",
            //    "Cars/{action}/{id}",
            //    new { controller = "Cars", action = "Index", id = UrlParameter.Optional },
            //    new[] { "Nop.Plugin.Tameion.Cars.Controllers" }
            //);
            //routes.Remove(route);
            //routes.Insert(0, route);
            
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
