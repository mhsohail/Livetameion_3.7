using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.DomainModels
{
    public class AffiliateProgramCustomer : BaseEntity
    {
        #region Foreign Keys
        public int AffiliateProgramId { get; set; }
        public int CustomerId { get; set; }
        #endregion

        #region Navigational Properties
        public virtual AffiliateProgram AffiliateProgram { get; set; }
        #endregion
    }
}
