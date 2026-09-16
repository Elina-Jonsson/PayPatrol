
using PayPatrol.Application.DTOs;
using PayPatrol.Domain.Entities;

namespace PayPatrol.Application.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllSubscriptionsByUserIdAsync(string userId);
        Task<Subscription?> GetSubscriptionByIdAsync(int id, string userId);
        Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
        Task<bool> UpdateSubscriptionAsync(Subscription subscription);
        Task<bool> DeleteSubscriptionAsync(int id, string userId);
    }
}
