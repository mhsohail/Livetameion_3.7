using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Nop.Plugin.Misc.Advertisements.Infrastructure;
using Nop.Plugin.Misc.Advertisements.Helpers;

namespace Nop.Plugin.Misc.Advertisements
{
    public class AdvertisementsPlugin : BasePlugin, IAdminMenuPlugin
    {
        private AdvertisementsContext _adsContext;

        public AdvertisementsPlugin(AdvertisementsContext adsContext)
        {
            _adsContext = adsContext;
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            // if plugin is not installed, run database creation script
            if (!PluginHelper.IsPluginInstalled())
            {
                this.Install();
            }

            var RootMenu = new SiteMapNode()
            {
                SystemName = "Misc.Advertisements",
                Title = "Advertisements",
                Visible = true,
                ChildNodes = {
                    new SiteMapNode()
                    {
                        SystemName = "Misc.Advertisements",
                        Title = "Manage Advertisements",
                        Url = "/Admin/Ads/Index",
                        ControllerName = "Ads",
                        ActionName = "Index",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", "Admin" } }
                    },

                    new SiteMapNode()
                    {
                        SystemName = "Misc.Advertisements",
                        Title = "Create Advertisement",
                        Url = "/Admin/Ads/Create",
                        ControllerName = "Ads",
                        ActionName = "Create",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
                    }
                }
            };
            
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Misc.Advertisements");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(RootMenu);
            else
                rootNode.ChildNodes.Add(RootMenu);
        }

        public override Core.Plugins.PluginDescriptor PluginDescriptor { get; set; }

        public override void Install()
        {
            try
            {
                _adsContext.Install();
            }
            catch (Exception) { }
            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                _adsContext.Uninstall();
            }
            catch (Exception) { }
            base.Uninstall();
        }
    }
}
