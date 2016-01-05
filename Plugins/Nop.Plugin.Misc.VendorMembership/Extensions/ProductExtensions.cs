using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Stores;
using Nop.Core.Infrastructure;
using Nop.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Extensions
{
    public static class ProductExtensions
    {
        public static List<Store> getStores(this Product product)
        {
            var storeMappingService = EngineContext.Current.Resolve<IStoreMappingService>();
            var storeService = EngineContext.Current.Resolve<IStoreService>();
            var storeMappingRepo = EngineContext.Current.Resolve<IRepository<StoreMapping>>();

            var storeMappings = storeMappingService.GetStoreMappings(product);
            //var storeMappings = storeMappingRepo.Table.Where(sm => sm.EntityId == product.Id).ToList();
            var stores = storeMappings.Select(sm => {
                return storeService.GetStoreById(sm.StoreId);
            }).ToList();

            return stores;
        }
    }
}
