using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Data;
using Nop.Data.Mapping.Catalog;
using Nop.Data.Mapping.Discounts;
using Nop.Plugin.Misc.AffiliateSystem.Helpers;
using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using Nop.Plugin.Tameion.AffiliateSystem.Infrastructure;
using Nop.Plugin.Tameion.AffiliateSystem.Maps;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Nop.Plugin.Tameion.AffiliateSystem.Infrastructure
{
    public class AffiliateSystemContext : DbContext, IDbContext
    {
        public AffiliateSystemContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        #region Implementation of IDbContext

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AffiliateMap());
            modelBuilder.Configurations.Add(new AffiliateProgramMap());
            modelBuilder.Configurations.Add(new AffiliateProgramStoreMap());
            modelBuilder.Configurations.Add(new AffiliateProgramCustomerMap());
            modelBuilder.Configurations.Add(new ProgramConditionMap());
            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            Database.SetInitializer<AffiliateSystemContext>(null);
            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }

        public void Uninstall()
        {
            var dbScript = "DROP TABLE " + PluginHelper.GetTableName<AffiliateProgramStore>(this);
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DROP TABLE " + PluginHelper.GetTableName<ProgramCondition>(this);
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DROP TABLE " + PluginHelper.GetTableName<AffiliateProgramCustomer>(this);
            Database.ExecuteSqlCommand(dbScript);
            
            dbScript = "DROP TABLE " + PluginHelper.GetTableName<AffiliateProgram>(this);
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DROP TABLE " + PluginHelper.GetTableName<Affiliate>(this);
            Database.ExecuteSqlCommand(dbScript);
            
            SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public System.Collections.Generic.IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool AutoDetectChangesEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }

        public bool ProxyCreationEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
