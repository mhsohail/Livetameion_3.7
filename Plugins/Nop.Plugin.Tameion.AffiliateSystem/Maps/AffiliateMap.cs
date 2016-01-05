using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Tameion.AffiliateSystem.Maps
{
    public class AffiliateMap : EntityTypeConfiguration<Affiliate>
    {
        public AffiliateMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("Affiliates");
            HasKey(aff => aff.Id);

            Property(aff => aff.Name);
        }
    }
}