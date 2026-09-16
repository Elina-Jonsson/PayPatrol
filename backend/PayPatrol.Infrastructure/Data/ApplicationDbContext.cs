using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayPatrol.Infrastructure.Identity;
using PayPatrol.Domain.Entities;

namespace PayPatrol.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ServiceCatalog> ServiceCatalogs => Set<ServiceCatalog>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Streaming" },
                new Category { Id = 2, Name = "Träning" },
                new Category { Id = 3, Name = "Mjukvara & Verktyg" },
                new Category { Id = 4, Name = "Musik" },
                new Category { Id = 5, Name = "Övrigt" }
             );
        }
    }
}
