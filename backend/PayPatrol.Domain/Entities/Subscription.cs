using System;
using System.Collections.Generic;
using System.Text;

namespace PayPatrol.Domain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public PaymentInterval Interval { get; set; }

        // FK to ServiceCatalog & navigation property
        public int ServiceCatalogId { get; set; }
        public ServiceCatalog ServiceCatalog { get; set; } = null!;

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
