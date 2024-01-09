using MainTz.Application.Models.UserEntities;
using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;

namespace MainTz.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        public NotificationRepository(IDbContextFactory<MainContext> dbContextFactory) 
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<NotificationEntity> GetNotificationByIdAndUserAsync(User user, int id)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationEntity = await context.Notifications.Where(notif =>
                    notif.UserId == user.Id &&
                    notif.Id == id).FirstOrDefaultAsync();

                return notificationEntity;
            }
        }
        public async Task<List<NotificationEntity>> GetNotificationsByUserAsync(User user)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationsEntity = await context.Notifications.Where(notif => notif.UserId == user.Id).ToListAsync();
                return notificationsEntity;
            }
        }
        public async Task<List<NotificationEntity>> GetNotificationsAsync()
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationsEntity = await context.Notifications.ToListAsync();
                return notificationsEntity;
            }
        }
        public async Task UpdateAsync(NotificationEntity notification)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Notifications.Update(notification);
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(NotificationEntity notification)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Notifications.Add(notification);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(NotificationEntity notification)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Notifications.Remove(notification);
                await context.SaveChangesAsync();
            }
        }
    }
}