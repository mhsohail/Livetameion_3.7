using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Plugin.Misc.VendorMembership.Services;
using Nop.Services.Common;
using Nop.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Extensions
{
    public static class VendorExtensions
    {
        public static void SetSubDomain(this Vendor vendor, string subdomain)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            genericAttributeService.SaveAttribute<string>(vendor, VendorAttributes.PreferredSubdomainName, subdomain);
        }

        public static string GetSubDomain(this Vendor vendor, string subdomain)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            return vendor.GetAttribute<string>(VendorAttributes.PreferredSubdomainName, genericAttributeService);
        }

        public static Store getStore(this Vendor vendor)
        {
            var _vendorStoreRepository = EngineContext.Current.Resolve<IRepository<VendorStore>>();
            var _storeRepository = EngineContext.Current.Resolve<IRepository<Store>>();

            var vendorStore = _vendorStoreRepository.Table.SingleOrDefault(vs => vs.VendorId == vendor.Id);
            return _storeRepository.GetById(vendorStore.StoreId);
        }

        public static void setStore(this Vendor vendor, Store store)
        {
            var _vendorStoreRepository = EngineContext.Current.Resolve<IRepository<VendorStore>>();
            var _storeRepository = EngineContext.Current.Resolve<IRepository<Store>>();

            var vendorStore = new VendorStore
            {
                StoreId = store.Id,
                VendorId = vendor.Id
            };

            _vendorStoreRepository.Insert(vendorStore);
        }

        public static Customer GetCustomer(this Vendor vendor)
        {
            var customerRepo = EngineContext.Current.Resolve<IRepository<Customer>>();
            return customerRepo.Table.SingleOrDefault(c => c.VendorId == vendor.Id);
        }
    }
}
