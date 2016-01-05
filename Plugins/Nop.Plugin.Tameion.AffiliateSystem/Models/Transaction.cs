using System;

namespace Nop.Plugin.Tameion.AffiliateSystem.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }

        public DateTime? TransactionTime { get; set; }

        public string AffiliateAccount { get; set; }

        public string TypeofTransaction { get; set; }

        public string TransactionDetail { get; set; }

        public int? Amount { get; set; }

        public int? AffiliateBalance { get; set; }
    }
}
