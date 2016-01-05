using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Core.Plugins;
using Nop.Plugin.Tameion.AffiliateSystem.Infrastructure;
using Nop.Services.Events;
using Nop.Web.Framework.Menu;
using System;

namespace Nop.Plugin.Tameion.AffiliateSystem
{
    public class PluginConfig : BasePlugin, IAdminMenuPlugin, IConsumer<EntityInserted<Order>>, IConsumer<EntityInserted<Customer>>
    {
        public override PluginDescriptor PluginDescriptor { get; set; }
        private AffiliateSystemContext _affiliateProgramContext;
        private readonly IWorkContext _workContext;

        public PluginConfig(AffiliateSystemContext affiliateProgramContext,
            IWorkContext workContext)
        {
            _affiliateProgramContext = affiliateProgramContext;
            _workContext = workContext;
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            //// if plugin is not installed, run database creation script
            //if (!PluginHelper.IsPluginInstalled())
            //{
            //    this.Install();
            //}

            //var RootMenu = new SiteMapNode()
            //{
            //    SystemName = "Misc.GroupDeals",
            //    Title = "Group Deals",
            //    Visible = true,
            //    ChildNodes = new List<SiteMapNode>
            //    {
            //        new SiteMapNode()
            //        {
            //            SystemName = "Misc.GroupDeals",
            //            Title = "Manage Group Deals",
            //            Url = "/Groupdeals/Index",
            //            ControllerName = "Groupdeals",
            //            ActionName = "Index",
            //            Visible = true,
            //            RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            //        },

            //        new SiteMapNode()
            //        {
            //            SystemName = "Misc.GroupDeals",
            //            Title = "Add New",
            //            Url = "/Groupdeals/Create",
            //            ControllerName = "Groupdeals",
            //            ActionName = "Create",
            //            Visible = true,
            //            RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            //        }
            //    }
            //};

            //var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Misc.GroupDeals");
            //if (pluginNode != null)
            //    pluginNode.ChildNodes.Add(RootMenu);
            //else
            //    rootNode.ChildNodes.Add(RootMenu);
        }

        public override void Install()
        {
            try
            {
                _affiliateProgramContext.Install();
            }
            catch (Exception) { }
            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                _affiliateProgramContext.Uninstall();
            }
            catch (Exception) { }
            base.Uninstall();
        }

        // this event method will execute after a new order is inserted into database
        public void HandleEvent(EntityInserted<Order> eventMessage)
        {
            // get the newly inserted order, which has AffiliateId
            var oder = eventMessage.Entity;
            var cartItems = _workContext.CurrentCustomer.ShoppingCartItems;
        }

        // this event method will execute after a new customer is inserted into database
        public void HandleEvent(EntityInserted<Customer> eventMessage)
        {
            // get the newly inserted customer, which has AffiliateId
            var customer = eventMessage.Entity;
        }
    }
}
