using Nop.Plugin.Misc.Advertisements.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.Advertisements
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Misc.Advertisements.AdvertisementsController",
                "Admin/Ads/{action}/{id}",
                new { area = "Admin", controller = "Advertisements", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.Advertisements.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
