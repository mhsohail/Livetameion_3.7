﻿using Nop.Core.Data;
using Nop.Core.Plugins;
using Nop.Plugin.Tameion.SupportTicketSystem.Helpers;
using Nop.Plugin.Tameion.SupportTicketSystem.Infrastructure;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.SupportTicketSystem
{
    public class PluginConfig : BasePlugin, IAdminMenuPlugin
    {
        SupportTicketSystemContext _supportTicketSystemContext;
        
        public PluginConfig(
            SupportTicketSystemContext supportTicketSystemContext
        )
        {
            _supportTicketSystemContext = supportTicketSystemContext;
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
                SystemName = "Tameion.SupportTicketSystem",
                Title = "Support Tickets",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
                ChildNodes = new List<SiteMapNode>
                {
                    new SiteMapNode()
                    {
                        SystemName = "Tameion.SupportTicketSystem",
                        Title = "Tickets",
                        Url = "Admin/Tickets/Index",
                        ControllerName = "Tickets",
                        ActionName = "Index",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
                    },

                    new SiteMapNode()
                    {
                        SystemName = "Tameion.SupportTicketSystem",
                        Title = "Create Ticket",
                        Url = "Admin/Tickets/Create",
                        ControllerName = "Tickets",
                        ActionName = "Create",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
                    }
                }
            };

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Tameion.SupportTicketSystem");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(RootMenu);
            else
                rootNode.ChildNodes.Add(RootMenu);
        }

        public bool Authenticate()
        {
            return true;
        }

        public override void Install()
        {
            try
            {
                _supportTicketSystemContext.Install();
            }
            catch (Exception) { }
            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                _supportTicketSystemContext.Uninstall();
            }
            catch (Exception) { }
            base.Uninstall();
        }

        public override PluginDescriptor PluginDescriptor { get; set; }
    }
    
}
