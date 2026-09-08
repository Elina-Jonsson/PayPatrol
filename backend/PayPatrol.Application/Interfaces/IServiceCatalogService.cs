using PayPatrol.Application.DTOs;

namespace PayPatrol.Application.Interfaces
{
    public interface IServiceCatalogService
    {
        Task<IEnumerable<ServiceCatalogDto>> GetAllServiceCatalogsAsync();
        Task<ServiceCatalogDto> GetServiceCatalogByIdAsync(int id);
        Task<IEnumerable<ServiceCatalogDto>> GetServiceCatalogsByCategoryIdAsync(int categoryId);
    }
}
