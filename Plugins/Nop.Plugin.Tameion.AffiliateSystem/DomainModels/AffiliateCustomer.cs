using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.DomainModels
{
    public class AffiliateCustomer : BaseEntity
    {
        #region Properties
        public int AffiliateId { get; set; }
        public int CustomerId { get; set; }
        #endregion
    }
}
