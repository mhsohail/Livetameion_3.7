using System.Web.Mvc;

namespace Nop.Plugin.Misc.Advertisements
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Advertisements_default",
                "Admin/Ads/{action}/{id}",
                new { controller = "Advertisements", action = "Index", area = "Admin", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.Advertisements.Controllers" }
            );
        }
    }
}
