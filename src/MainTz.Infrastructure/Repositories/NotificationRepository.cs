using MainTz.Application.Models.UserEntities;
using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
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
        public async Task<NotificationEntity> GetNotificationByIdAndUserAsync(User user, int id)
        {
            var notificationEntity = await _context.Notifications.Where(notif =>
                notif.UserId == user.Id &&
                notif.Id == id).FirstOrDefaultAsync();

            return notificationEntity;
        }
        public async Task<List<NotificationEntity>> GetNotificationsByUserAsync(User user)
        {
            var notificationsEntity = await _context.Notifications.Where(notif => notif.UserId == user.Id).ToListAsync();
            return notificationsEntity;
        }
        public async Task<List<NotificationEntity>> GetNotificationsAsync()
        {
            var notificationsEntity = await _context.Notifications.ToListAsync();
            return notificationsEntity;
        }
        public async Task UpdateAsync(NotificationEntity notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(NotificationEntity notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(NotificationEntity notification)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }
}