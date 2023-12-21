using MainTz.Application.Repositories;
using MainTz.Database.Entities;
using MainTa.Database.Context;

namespace MainTz.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MainContext _context;
        public NotificationRepository(MainContext context) 
        {
            _context = context;
        }
        public async Task UpdateNotificationAsync(NotificationEntity notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }
    }
}