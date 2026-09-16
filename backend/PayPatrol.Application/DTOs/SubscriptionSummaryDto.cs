
namespace PayPatrol.Application.DTOs
{
    public class SubscriptionSummaryDto
    {
        public decimal TotalMonthlyCost { get; set; }
        public decimal TotalYearlyCost { get; set; }
        public List<CategoryCostDto> CostsByCategory { get; set; } = new List<CategoryCostDto>();

        public class CategoryCostDto
        {
            public string CategoryName { get; set; } = null!;
            public decimal MonthlyCost { get; set; }
            public decimal YearlyCost { get; set; }
        }

    }
}
