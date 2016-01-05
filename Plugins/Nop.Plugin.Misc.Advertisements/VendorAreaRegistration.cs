using System.Web.Mvc;

namespace Nop.Plugin.Misc.Advertisements
{
    public class VendorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Vendor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Vendor_Advertisements_default",
                "Vendor/Ads/{action}/{id}",
                new { controller = "Advertisements", action = "Index", area = "Vendor", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.Advertisements.Controllers" }
            );
        }
    }
}
