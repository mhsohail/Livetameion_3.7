using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.DomainModels
{
    public class AffiliateProgram : BaseEntity
    {
        #region ctor
        public AffiliateProgram()
        {
            StartDateTimeUtc = DateTime.UtcNow;
            EndDateTimeUtc = DateTime.UtcNow;
        }
        #endregion

        #region properties
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int StatusId { get; set; }
        public ProgramStatus Status { get { return (ProgramStatus)StatusId; } set { StatusId = (int)value; } }
        public DateTime StartDateTimeUtc { get; set; }
        public DateTime EndDateTimeUtc { get; set; }
        public decimal TotalCommission { get; set; }
        public bool NotifyAffiliatesOnChange { get; set; }
        #endregion

        #region virtual properties
        public virtual ICollection<AffiliateProgramStore> AffiliateProgramStores { get; set; }
        public virtual ICollection<AffiliateProgramCustomer> AffiliateProgramCustomers { get; set; }
        public virtual ICollection<ProgramCondition> Conditions { get; set; }
        #endregion
    }
}
