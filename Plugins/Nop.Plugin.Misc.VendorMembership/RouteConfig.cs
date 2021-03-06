﻿using Nop.Plugin.Misc.VendorMembership.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.VendorMembership
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            //var route = routes.MapRoute("Plugin.Misc.VendorMembership.VendorMembershipController",
            //    "VendorMembership/{action}/{id}",
            //    new { controller = "VendorMembership", action = "Dashboard", id = UrlParameter.Optional },
            //    new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            //);
            //routes.Remove(route);
            //routes.Insert(0, route);

            var route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.ProductsController",
                "Vendor/Products/{action}/{id}",
                new { area = "Vendor", controller = "Products", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.OrdersController",
                "Vendor/Orders/{action}/{id}",
                new { area = "Vendor", controller = "Orders", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.AccountController",
                "Vendor/Account/{action}",
                new { area = "Vendor", controller = "Account", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);
            
            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.Dashboard",
                "Vendor",
                new { area = "Vendor", controller = "Dashboard", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.SettingsController",
                "Vendor/Settings",
                new { area = "Vendor", controller = "Settings", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.InvoicesController",
                "Vendor/Invoices/{action}",
                new { area = "Vendor", controller = "Invoices", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Admin.MultitenancyController",
                "Admin/Multitenancy/{action}",
                new { area = "Admin", controller = "Multitenancy", action = "Settings" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Admin.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Admin.VendorTypesController",
                "Admin/VendorTypes/{action}",
                new { area = "Admin", controller = "VendorTypes", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Admin.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Theme.Controllers",
                "Theme/Home/{action}/{id}",
                new { area = "Theme", controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Theme.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Theme.Controllers",
                "Theme",
                new { area = "Theme", controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Theme.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Areas.Vendor.Controllers.ReturnRequestController",
                "Vendor/ReturnRequest/{action}",
                new { area = "Vendor", controller = "ReturnRequest", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Areas.Vendor.Controllers.DashboardController",
                "Vendor/Dashboard/{action}",
                new { area = "Vendor", controller = "Dashboard", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
