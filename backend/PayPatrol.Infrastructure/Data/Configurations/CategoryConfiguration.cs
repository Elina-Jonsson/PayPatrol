using Microsoft.EntityFrameworkCore;
using PayPatrol.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayPatrol.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        { 
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
