using MainTz.Application.Models;

namespace MainTz.Application.Services
{
    public interface INotificationService
    {
        public Task<List<Notification>> GetNotificationsByUserAsync(User user);
        public Task<Notification> GetNotificationByIdAndUserWithMarkedAsync(User user, int id);
        public Task<bool> UpdateAsync(Notification notification);
        public Task<bool> CreateAsync(Notification notification);
        public Task<bool> DeleteAsync(Notification notification);
    }
}