
using PayPatrol.Domain.Entities;

namespace PayPatrol.Application.DTOs
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public PaymentInterval Interval { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Title { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}
