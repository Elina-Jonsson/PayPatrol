using Microsoft.EntityFrameworkCore;
using PayPatrol.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayPatrol.Infrastructure.Data.Configurations
{
    public class ServiceCatalogConfiguration : IEntityTypeConfiguration<ServiceCatalog>
    {
        public void Configure(EntityTypeBuilder<ServiceCatalog> builder)
        {
            builder.Property(sc => sc.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(sc => sc.Category)
                .WithMany(c => c.ServiceCatalogs)
                .HasForeignKey(sc => sc.CategoryId);

        }
    }
}
