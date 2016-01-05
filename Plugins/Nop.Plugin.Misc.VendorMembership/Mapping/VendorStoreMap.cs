using Nop.Plugin.Misc.VendorMembership.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    public class VendorStoreMap : EntityTypeConfiguration<VendorStore>
    {
        public VendorStoreMap()
        {
            ToTable("Vendor_Store_Mapping");
            HasKey(vs => vs.Id);

            Property(vs => vs.VendorId);
            Property(vs => vs.StoreId);
        }
    }
}
