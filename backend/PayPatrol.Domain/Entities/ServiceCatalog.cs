namespace PayPatrol.Domain.Entities
{
    public class ServiceCatalog
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;

        // FK to Category & navigation property
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // Navigation property for Subscriptions
        //public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
