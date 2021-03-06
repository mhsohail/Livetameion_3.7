using System;

namespace Nop.Plugin.Tameion.AffiliateSystem.ViewModels
{
    public class ComissionViewModel
    {
        public long Id { get; set; }

        public DateTime? TransactionTime { get; set; }

        public string CommissionType { get; set; }

        public string AffiliateAccount { get; set; }

        public string AffiliateComission { get; set; }

        public int? CustomerDiscount { get; set; }

        public int? PurchaseTotal { get; set; }

        public string DetailStatus { get; set; }
    }
}
