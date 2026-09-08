using PayPatrol.Application.DTOs;

namespace PayPatrol.Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsByUserIdAsync(string userId);
        Task<SubscriptionDto?> GetSubscriptionByIdAsync(int id, string userId);
        Task<SubscriptionDto> CreateSubscriptionsAsync(CreateSubscriptionDto dto, string userId);

        Task<bool> UpdateSubscriptionAsync(CreateSubscriptionDto dto, string userId, int id);
        Task<bool> DeleteSubscriptionAsync(int id, string userId);
    }
}
