using MainTz.Application.Models.UserEntities;
using MainTz.Database.Entities;

namespace MainTz.Application.Repositories
{
    public interface INotificationRepository
    {
        public Task<NotificationEntity> GetNotificationByIdAndUserAsync(User user, int id);
        public Task<List<NotificationEntity>> GetNotificationsByUserAsync(User user);
        public Task<List<NotificationEntity>> GetNotificationsAsync();
        public Task UpdateAsync(NotificationEntity notification);
        public Task CreateAsync(NotificationEntity notification);
        public Task DeleteAsync(NotificationEntity notification);
    }
}