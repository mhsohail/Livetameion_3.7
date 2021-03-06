﻿using System.Web.Mvc;

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
                "Admin_Tickets_default",
                "Admin/Tickets/{action}/{id}",
                new { controller = "Tickets", action = "Index", area = "Admin", id = "" },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Admin.Controllers" }
            );
        }
    }
}
