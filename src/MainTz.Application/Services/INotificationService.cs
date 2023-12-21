using MainTz.Application.Models.UserEntities;

namespace MainTz.Application.Services
{
    public interface INotificationService
    {
        public Task<bool> GetNotificationByUser(User user);
    }
}
