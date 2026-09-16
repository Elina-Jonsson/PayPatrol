using Microsoft.EntityFrameworkCore;
using PayPatrol.Application.Interfaces;
using PayPatrol.Domain.Entities;
using PayPatrol.Infrastructure.Data;

namespace PayPatrol.Infrastructure.Repositories
{
    public class ServiceCatalogRepository : IServiceCatalogRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceCatalogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceCatalog>> GetAllAsync()
        {
            return await _context.ServiceCatalogs
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ServiceCatalog?> GetByIdAsync(int id)
        {
            return await _context.ServiceCatalogs
                .AsNoTracking()
                .FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<IEnumerable<ServiceCatalog>> GetCategoryIdAsync(int categoryId)
        {
            return await _context.ServiceCatalogs
                .AsNoTracking()
                .Where(sc => sc.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
