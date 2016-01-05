using System.Collections.Generic;

namespace Nop.Plugin.Tameion.AffiliateSystem.Models
{
    public partial class Affiliate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Affiliate()
        {
            Affiliates1 = new HashSet<Affiliate>();
        }
        
        public long Id { get; set; }

        public string Name { get; set; }

        public string AffiliateAccount { get; set; }

        public long? Parent { get; set; }

        public long? Group { get; set; }

        public int? TotalCommission { get; set; }

        public int? TotalPaidOut { get; set; }

        public int? CurrentBalance { get; set; }

        public string WebsiteStatus { get; set; }

        public string Website { get; set; }

        public string WithdrawalRequestMethod { get; set; }

        public string Autopaymentwhenaccountbalancereaches { get; set; }

        public string ReverseLevel { get; set; }

        public bool? Active { get; set; }

        public virtual AffiliateGroup AffiliateGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Affiliate> Affiliates1 { get; set; }

        public virtual Affiliate Affiliate1 { get; set; }
    }
}
