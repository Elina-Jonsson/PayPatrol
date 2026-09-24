namespace PayPatrol.Domain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public PaymentInterval Interval { get; set; }
        public string Title { get; set; } = null!;

        // FK to ServiceCatalog & navigation property
        public int? ServiceCatalogId { get; set; }
        public ServiceCatalog? ServiceCatalog { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // FK to User
        public string UserId { get; set; } = null!;

    }

    public enum PaymentInterval
    {
        Weekly,
        Monthly,
        Yearly
    }
}
