using System.Collections.Generic;

namespace Nop.Plugin.Tameion.AffiliateSystem.Models
{
    public partial class AffiliateGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AffiliateGroup()
        {
            Affiliates = new HashSet<Affiliate>();
        }

        public long Id { get; set; }

        public string GroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Affiliate> Affiliates { get; set; }
    }
}
