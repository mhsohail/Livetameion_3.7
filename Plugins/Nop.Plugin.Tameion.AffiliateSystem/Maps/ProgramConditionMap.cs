using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Tameion.AffiliateSystem.Maps
{
    public class ProgramConditionMap : EntityTypeConfiguration<ProgramCondition>
    {
        public ProgramConditionMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("AffiliateProgramConditions");
            HasKey(aff => aff.Id);

            Property(aff => aff.Name);
            Property(aff => aff.AffiliateProgramId);
            Property(aff => aff.OperationId);
            Property(aff => aff.Value);
        }
    }
}