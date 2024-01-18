using MainTz.Application.Models.UserEntities;

namespace MainTz.Application.Repositories
{
    public interface INotificationRepository
    {
        public Task<Notification> GetNotificationByIdAndUserAsync(User user, int id);
        public Task<List<Notification>> GetNotificationsByUserAsync(User user);
        public Task<List<Notification>> GetNotificationsAsync();
        public Task UpdateAsync(Notification notification);
        public Task CreateAsync(Notification notification);
        public Task DeleteAsync(Notification notification);
    }
}