using System;
using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Tameion.Cars.Infrastructure;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Tameion.AffiliateSystem.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CARS_CONTEXT_NAME = "nop_object_context_product_view_cars";

        /*
         * Don't register core models here, they should stay registered in NOP core context
         * The below Register() mehtod is called at the following moments:
         * 1. When application starts
         * 2. During plugin installation
         * 3. During plugin uninstallation
         */
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //data context
            this.RegisterPluginDataContext<CarsContext>(builder, CARS_CONTEXT_NAME);
            RegisterServices(builder);
        }
        
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //data context
            this.RegisterPluginDataContext<CarsContext>(builder, CARS_CONTEXT_NAME);
            RegisterServices(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            // repositories
            //override required repository with our custom context
            //builder.RegisterType<EfRepository<Affiliate>>()
            //    .As<IRepository<Affiliate>>()
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CARS_CONTEXT_NAME))
            //    .InstancePerLifetimeScope();

            ///////////////////////////////////////////////////////////////////////////////////////////////

            // services
            //builder.RegisterType<AffiliateService>().As<IAffiliateService>().InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
