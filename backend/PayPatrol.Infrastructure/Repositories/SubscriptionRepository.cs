using Microsoft.EntityFrameworkCore;
using PayPatrol.Application.Interfaces;
using PayPatrol.Domain.Entities;
using PayPatrol.Infrastructure.Data;

namespace PayPatrol.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscription>> GetAllSubscriptionsByUserIdAsync(string userId)
        {
            return await _context.Subscriptions
                .Include(s => s.Category)
                .Where(s => s.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Subscription?> GetSubscriptionByIdAsync(int id, string userId)
        {
            return await _context.Subscriptions
                .Include(s => s.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
        }

        public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<bool> UpdateSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSubscriptionAsync(int id, string userId)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (subscription == null) return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
