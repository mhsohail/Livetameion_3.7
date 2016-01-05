using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class VendorStore : BaseEntity
    {
        public int VendorId { get; set; }
        public int StoreId { get; set; }
    }
}
