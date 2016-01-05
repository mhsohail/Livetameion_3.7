using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.Maps
{
    public class AffiliateProgramMap : EntityTypeConfiguration<AffiliateProgram>
    {
        public AffiliateProgramMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("AffiliatePrograms");
            HasKey(aff => aff.Id);

            Property(aff => aff.Name);
            Property(aff => aff.Description);
            Property(aff => aff.EndDateTimeUtc);
            Property(aff => aff.Priority);
            Property(aff => aff.StartDateTimeUtc);
            Property(aff => aff.Status);
            Property(aff => aff.StatusId);
            Property(aff => aff.TotalCommission);
        }
    }
}
