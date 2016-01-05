using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Misc.Advertisements.DomainModels;
using Nop.Plugin.Misc.Advertisements.Services;
using Nop.Core.Configuration;
using System;

namespace Nop.Plugin.Misc.Advertisements.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string ADS_CONTEXT_NAME = "nop_object_context_product_view_ads";

        /*
         * Don't register core models here, they should stay registered in NOP core context
         * The below Register() mehtod is called at the following moments:
         * 1. When application starts
         * 2. During plugin installation
         * 3. During plugin uninstallation
         */
        
        // Register method for v3.6
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //data context
            this.RegisterPluginDataContext<AdvertisementsContext>(builder, ADS_CONTEXT_NAME);
            RegisterServices(builder);
        }

        // Register method for v3.7
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //data context
            this.RegisterPluginDataContext<AdvertisementsContext>(builder, ADS_CONTEXT_NAME);
            RegisterServices(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            // repositories
            //override required repository with our custom context
            builder.RegisterType<EfRepository<Advertisement>>()
                .As<IRepository<Advertisement>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ADS_CONTEXT_NAME))
                .InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////

            // services
            builder.RegisterType<AdvertisementService>().As<IAdvertisementService>().InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
