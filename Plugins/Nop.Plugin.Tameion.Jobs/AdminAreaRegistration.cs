using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem
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
                "Admin_Jobs_default",
                "Admin/Jobs/{action}/{id}",
                new { controller = "Jobs", action = "Index", area = "Admin", id = "" },
                new[] { "Nop.Plugin.Tameion.Jobs.Areas.Admin.Controllers" }
            );
        }
    }
}
