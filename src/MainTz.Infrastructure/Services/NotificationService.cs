using MainTz.Application.Models.UserEntities;
using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;
        private readonly IMapper _mapper;
        public NotificationService(IMapper mapper, INotificationRepository notificationRepository, ILogger<NotificationService> logger) 
        {
            _logger = logger;
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }
        public async Task<Notification> GetNotificationByIdAndUserWithMarkedAsync(User user, int id)
        {
            var notificatonEntity = await _notificationRepository.GetNotificationByIdAndUserAsync(user, id);
            notificatonEntity.IsRead = true;
            await _notificationRepository.UpdateAsync(notificatonEntity);
            var notification = _mapper.Map<Notification>(notificatonEntity);
            return notification;
        }
        public async Task<List<Notification>> GetNotificationsByUserAsync(User user)
        {
            var notificationsEntity = await _notificationRepository.GetNotificationsByUserAsync(user);
            var notifications = _mapper.Map<List<Notification>>(notificationsEntity);
            return notifications;
        }
        public async Task<bool> UpdateAsync(Notification notification)
        {
            try
            {
                var notificationEntity = _mapper.Map<NotificationEntity>(notification);
                await _notificationRepository.UpdateAsync(notificationEntity);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> CreateAsync(Notification notification)
        {
            try
            {
                var notificationEntity = _mapper.Map<NotificationEntity>(notification);
                await _notificationRepository.UpdateAsync(notificationEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(Notification notification)
        {
            try
            {
                var notificationEntity = _mapper.Map<NotificationEntity>(notification);
                await _notificationRepository.UpdateAsync(notificationEntity);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
    }
}
