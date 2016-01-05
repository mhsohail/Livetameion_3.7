using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership
{
    public class ThemeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Theme";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            var route = context.MapRoute(
                "Theme_default",
                "Theme/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = "Theme", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Theme.Controllers" }
            );
            context.Routes.Remove(route);
            context.Routes.Insert(0, route);
        }
    }
}
