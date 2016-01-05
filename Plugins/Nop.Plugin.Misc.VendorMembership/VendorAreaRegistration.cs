using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership
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
                "Vendor_default",
                "Vendor/{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", area = "Vendor", id = UrlParameter.Optional },
                new[] {
                    "Nop.Plugin.Misc.VendorMembership.Controllers",
                    "Nop.Plugin.Misc.VendorMembership.Areas.Vendor.Controllers"
                }
            );
        }
    }
}
