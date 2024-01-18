using MainTz.Application.Models.UserEntities;
using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;

namespace MainTz.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(INotificationRepository notificationRepository, ILogger<NotificationService> logger) 
        {
            _logger = logger;
            _notificationRepository = notificationRepository;
        }
        public async Task<Notification> GetNotificationByIdAndUserWithMarkedAsync(User user, int id)
        {
            var notification = await _notificationRepository.GetNotificationByIdAndUserAsync(user, id);
            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification);
            return notification;
        }
        public async Task<List<Notification>> GetNotificationsByUserAsync(User user)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserAsync(user);
            return notifications;
        }
        public async Task<bool> UpdateAsync(Notification notification)
        {
            try
            {
                await _notificationRepository.UpdateAsync(notification);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<bool> CreateAsync(Notification notification)
        {
            try
            {
                await _notificationRepository.UpdateAsync(notification);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteAsync(Notification notification)
        {
            try
            {
                await _notificationRepository.UpdateAsync(notification);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
    }
}
