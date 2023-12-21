using MainTz.Database.Entities;

namespace MainTz.Application.Repositories
{
    public interface INotificationRepository
    {
        public Task UpdateNotificationAsync(NotificationEntity notification);
    }
}