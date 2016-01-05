using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Areas.Vendor.ViewModels
{
    public class DashboardModel : BaseNopModel
    {
        public IList<Product> ActiveGroupDeals { get; set; }
    }
}
