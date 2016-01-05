using System.Data.Entity;

namespace Nop.Plugin.Tameion.AffiliateSystem.Models
{
    public partial class AffiliatesDB : DbContext
    {
        public AffiliatesDB()
            : base("name=AffiliatesDB")
        {
        }

        public virtual DbSet<AffiliateGroup> AffiliateGroups { get; set; }
        public virtual DbSet<Affiliate> Affiliates { get; set; }
        public virtual DbSet<Comission> Comissions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Withdrawal> Withdrawals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AffiliateGroup>()
                .HasMany(e => e.Affiliates)
                .WithOptional(e => e.AffiliateGroup)
                .HasForeignKey(e => e.Group);

            modelBuilder.Entity<Affiliate>()
                .Property(e => e.AffiliateAccount)
                .IsFixedLength();

            modelBuilder.Entity<Affiliate>()
                .Property(e => e.Autopaymentwhenaccountbalancereaches)
                .IsFixedLength();

            modelBuilder.Entity<Affiliate>()
                .HasMany(e => e.Affiliates1)
                .WithOptional(e => e.Affiliate1)
                .HasForeignKey(e => e.Parent);
        }
    }
}
