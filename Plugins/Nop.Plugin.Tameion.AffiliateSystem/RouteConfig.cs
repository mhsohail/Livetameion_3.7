using Nop.Plugin.Tameion.AffiliateSystem.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.AffiliateSystem
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Tameion.AffiliateSystem.AffiliatesController",
                "Affiliates/{action}/{id}",
                new { area = "Admin", controller = "Affiliates", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.AffiliateSystem.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);
            
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
