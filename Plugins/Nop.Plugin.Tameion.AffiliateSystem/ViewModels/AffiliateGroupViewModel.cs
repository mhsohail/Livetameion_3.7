using System.Collections.Generic;

namespace Nop.Plugin.Tameion.AffiliateSystem.ViewModels
{
    public partial class AffiliateGroupViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AffiliateGroupViewModel()
        {
            Affiliates = new HashSet<AffiliateViewModel>();
        }

        public long Id { get; set; }

        public string GroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<AffiliateViewModel> Affiliates { get; set; }
    }
}
