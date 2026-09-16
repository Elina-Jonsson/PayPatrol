using PayPatrol.Domain.Entities;

namespace PayPatrol.Application.Interfaces
{
    public interface IServiceCatalogRepository
    {
        Task<IEnumerable<ServiceCatalog>> GetAllAsync();
        Task<ServiceCatalog?> GetByIdAsync(int id);
        Task<IEnumerable<ServiceCatalog>> GetCategoryIdAsync(int categoryId);
    }
}