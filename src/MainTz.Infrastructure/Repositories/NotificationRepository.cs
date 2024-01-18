using MainTz.Application.Models.UserEntities;
using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using AutoMapper;

namespace MainTz.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;
        public NotificationRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper) 
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }
        public async Task<Notification> GetNotificationByIdAndUserAsync(User user, int id)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationEntity = await context.Notifications.Where(notif =>
                    notif.UserId == user.Id &&
                    notif.Id == id).FirstOrDefaultAsync();
                var notification = _mapper.Map<Notification>(notificationEntity);
                return notification;
            }
        }
        public async Task<List<Notification>> GetNotificationsByUserAsync(User user)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationEntities = await context.Notifications.Where(notif => notif.UserId == user.Id).ToListAsync();
                var notifications = _mapper.Map<List<Notification>>(notificationEntities);
                return notifications;
            }
        }
        public async Task<List<Notification>> GetNotificationsAsync()
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationEntities = await context.Notifications.ToListAsync();
                var notifications = _mapper.Map<List<Notification>>(notificationEntities);
                return notifications;
            }
        }
        public async Task UpdateAsync(Notification notification)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationEntity = _mapper.Map<NotificationEntity>(notification);
                context.Notifications.Attach(notificationEntity);
                context.Notifications.Update(notificationEntity);
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(Notification notification)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationEntity = _mapper.Map<NotificationEntity>(notification);
                context.Notifications.Add(notificationEntity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Notification notification)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var notificationEntity = _mapper.Map<NotificationEntity>(notification);
                context.Notifications.Remove(notificationEntity);
                await context.SaveChangesAsync();
            }
        }
    }
}