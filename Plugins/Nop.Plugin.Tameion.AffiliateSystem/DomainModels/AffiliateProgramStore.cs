using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.DomainModels
{
    public class AffiliateProgramStore : BaseEntity
    {
        #region properties
        public int AffiliateProgramId { get; set; }
        public int StoreId { get; set; }
        #endregion

        #region virtual properties
        public virtual AffiliateProgram AffiliateProgram { get; set; }
        #endregion
    }
}
