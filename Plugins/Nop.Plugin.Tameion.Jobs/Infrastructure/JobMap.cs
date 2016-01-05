using Nop.Plugin.Tameion.Jobs.DomainModels;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Tameion.Jobs.Infrastructure
{
    public class JobMap : EntityTypeConfiguration<Job>
    {
        public JobMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("Jobs");
            HasKey(j => j.Id);

            Property(j => j.Name);
        }
    }
}
