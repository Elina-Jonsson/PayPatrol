using System;
using System.Collections.Generic;
using System.Text;

namespace PayPatrol.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Navigation property for ServiceCatalogs FK
        public ICollection<ServiceCatalog> ServiceCatalogs { get; set; } = new List<ServiceCatalog>();
    }
}
