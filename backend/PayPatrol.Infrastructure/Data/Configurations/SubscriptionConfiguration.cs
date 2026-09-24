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

            builder.HasOne(s => s.Category)
                .WithMany()
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.ServiceCatalog)
                .WithMany()
                .HasForeignKey(s => s.ServiceCatalogId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
