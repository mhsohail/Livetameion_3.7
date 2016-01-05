using Nop.Plugin.Tameion.Jobs.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.Auctions
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Tameion.Jobs.JobsController",
                "Jobs/{action}/{id}",
                new { controller = "Jobs", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.Jobs.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);
            
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
