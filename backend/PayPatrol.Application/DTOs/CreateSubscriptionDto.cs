using PayPatrol.Domain.Entities;

namespace PayPatrol.Application.DTOs
{
    public class CreateSubscriptionDto
    {
        public decimal Amount { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public PaymentInterval Interval { get; set; }
        public int ServiceCatalogId { get; set; }
    }
}
