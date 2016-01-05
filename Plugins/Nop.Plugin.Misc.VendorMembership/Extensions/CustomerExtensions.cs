using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services.Customers;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Extensions
{
    public static class CustomerExtensions
    {
        public static void SetVendor(this Customer customer, Vendor vendor)
        {
            var customerService = EngineContext.Current.Resolve<ICustomerService>();
            customer.VendorId = vendor.Id;
            customerService.UpdateCustomer(customer);
        }

        public static Vendor GetVendor(this Customer customer)
        {
            var vendorService = EngineContext.Current.Resolve<IVendorService>();
            return vendorService.GetVendorById(customer.VendorId);
        }
    }
}
