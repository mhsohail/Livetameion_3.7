using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem
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
                "Vendor_Jobs_default",
                "Vendor/Jobs/{action}/{id}",
                new { controller = "Jobs", action = "Index", area = "Vendor", id = "" },
                new[] { "Nop.Plugin.Tameion.Jobs.Areas.Vendor.Controllers" }
            );
        }
    }
}
