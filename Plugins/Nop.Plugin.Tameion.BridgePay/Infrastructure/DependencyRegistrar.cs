using System;
using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Tameion.BridgePay.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string BRIDGE_PAY_CONTEXT_NAME = "nop_object_context_product_view_bridge_pay";

        /*
         * Don't register core models here, they should stay registered in NOP core context
         * The below Register() mehtod is called at the following moments:
         * 1. When application starts
         * 2. During plugin installation
         * 3. During plugin uninstallation
         */

        // Register method for v3.60
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //data context
            this.RegisterPluginDataContext<BridgePayContext>(builder, BRIDGE_PAY_CONTEXT_NAME);
        }

        // Register method for v3.70
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //data context
            this.RegisterPluginDataContext<BridgePayContext>(builder, BRIDGE_PAY_CONTEXT_NAME);
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
