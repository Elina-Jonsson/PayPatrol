using PayPatrol.Application.DTOs;
using PayPatrol.Application.Interfaces;
using PayPatrol.Domain.Entities;
using static PayPatrol.Application.DTOs.SubscriptionSummaryDto;

namespace PayPatrol.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _repository;

        public SubscriptionService(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByUserIdAsync(string userId)
        {
            var subscriptions = await _repository.GetAllSubscriptionsByUserIdAsync(userId);
            return subscriptions.Select(s => new SubscriptionDto
            {
                Id = s.Id,
                Amount = s.Amount,
                NextPaymentDate = s.NextPaymentDate,
                Interval = s.Interval,
                ServiceCatalogId = s.ServiceCatalogId,
                ServiceName = s.ServiceCatalog.Name,
                UserId = s.UserId
            });
        }

        public async Task<SubscriptionDto?> GetSubscriptionByIdAsync(int id, string userId)
        {
            var subscription = await _repository.GetSubscriptionByIdAsync(id, userId);
            if (subscription == null)
            {
                return null;
            }
            return new SubscriptionDto
            {
                Id = subscription.Id,
                Amount = subscription.Amount,
                NextPaymentDate = subscription.NextPaymentDate,
                Interval = subscription.Interval,
                ServiceCatalogId = subscription.ServiceCatalogId,
                ServiceName = subscription.ServiceCatalog.Name,
                UserId = subscription.UserId
            };
        }

        public async Task<SubscriptionDto> CreateSubscriptionsAsync(CreateSubscriptionDto dto, string userId)
        {
            var subscription = new Subscription
            {
                Amount = dto.Amount,
                NextPaymentDate = dto.NextPaymentDate,
                Interval = dto.Interval,
                ServiceCatalogId = dto.ServiceCatalogId,
                UserId = userId
            };
            var createdSubscription = await _repository.CreateSubscriptionAsync(subscription);

            return new SubscriptionDto
            {
                Id = createdSubscription.Id,
                Amount = createdSubscription.Amount,
                NextPaymentDate = createdSubscription.NextPaymentDate,
                Interval = createdSubscription.Interval,
                ServiceCatalogId = createdSubscription.ServiceCatalogId,
                ServiceName = createdSubscription.ServiceCatalog.Name,
                UserId = createdSubscription.UserId
            };
        }

        public async Task<bool> UpdateSubscriptionAsync(CreateSubscriptionDto dto, string userId, int id)
        {
            var existingSubscription = await _repository.GetSubscriptionByIdAsync(id, userId);
            if (existingSubscription == null)
            {
                return false;
            }

            if (dto.Amount <= 0)
            {
                return false;
            }

            existingSubscription.Amount = dto.Amount;
            existingSubscription.NextPaymentDate = dto.NextPaymentDate;
            existingSubscription.Interval = dto.Interval;
            existingSubscription.ServiceCatalogId = dto.ServiceCatalogId;

            return await _repository.UpdateSubscriptionAsync(existingSubscription);
        }

        public async Task<bool> DeleteSubscriptionAsync(int id, string userId)
        {
            return await _repository.DeleteSubscriptionAsync(id, userId);
        }

        public async Task<SubscriptionSummaryDto> GetSubscriptionSummaryAsync(string userId)
        {
            var userSubscriptions = await _repository.GetAllSubscriptionsByUserIdAsync(userId);

            decimal CalculateMonthlyCost(decimal amount, PaymentInterval interval)
            {
                return interval == PaymentInterval.Yearly ? amount / 12m : amount;
            }

            var totalMonthly = userSubscriptions.Sum(s => CalculateMonthlyCost(s.Amount, s.Interval));

            var costsByCategory = userSubscriptions
                .GroupBy(s => s.ServiceCatalog?.Category?.Name ?? "Övrigt")
                .Select(group => new CategoryCostDto
                {
                    CategoryName = group.Key,
                    MonthlyCost = Math.Round(group.Sum(s => CalculateMonthlyCost(s.Amount, s.Interval)), 2),
                    YearlyCost = Math.Round(group.Sum(s => s.Interval == PaymentInterval.Yearly ? s.Amount : s.Amount * 12m), 2)
                })
                .OrderByDescending(c => c.MonthlyCost)
                .ToList();

            return new SubscriptionSummaryDto
            {
                TotalMonthlyCost = Math.Round(totalMonthly, 2),
                TotalYearlyCost = Math.Round(totalMonthly * 12m, 2),
                CostsByCategory = costsByCategory
            };
        }
    }
}
