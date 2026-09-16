using PayPatrol.Application.Interfaces;
using PayPatrol.Application.DTOs;

namespace PayPatrol.Application.Services
{
    public class ServiceCatalogService : IServiceCatalogService
    {
        private readonly IServiceCatalogRepository _repository;

        public ServiceCatalogService(IServiceCatalogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceCatalogDto>> GetAllServiceCatalogsAsync()
        {
            var services = await _repository.GetAllAsync();
            return services.Select(s => new ServiceCatalogDto
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.CategoryId,
                CategoryName = s.Category.Name
            });
        }

        public async Task<ServiceCatalogDto?> GetServiceCatalogByIdAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);
            if (service == null)
            {
                return null;
            }
            return new ServiceCatalogDto
            {
                Id = service.Id,
                Name = service.Name,
                CategoryId = service.CategoryId,
                CategoryName = service.Category.Name
            };
        }

        public async Task<IEnumerable<ServiceCatalogDto>> GetServiceCatalogsByCategoryIdAsync(int categoryId)
        {
            var services = await _repository.GetCategoryIdAsync(categoryId);

            return services.Select(s => new ServiceCatalogDto
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.CategoryId,
                CategoryName = s.Category.Name
            });
        } 
    }
}
