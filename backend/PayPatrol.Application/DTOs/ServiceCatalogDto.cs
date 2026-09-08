
namespace PayPatrol.Application.DTOs
{
    public class ServiceCatalogDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

    }
}
