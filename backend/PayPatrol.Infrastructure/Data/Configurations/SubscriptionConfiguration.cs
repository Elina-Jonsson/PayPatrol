using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayPatrol.Domain.Entities;

namespace PayPatrol.Infrastructure.Data.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.Property(s => s.Amount)
                .HasPrecision(10, 2);

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.HasOne(s => s.ServiceCatalog)
                .WithMany(s => s.Subscriptions)
                .HasForeignKey(s => s.ServiceCatalogId);
        }
    }
}
