using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        public NotificationService(INotificationRepository notificationRepository, 
            ILogger<NotificationService> logger, ICarRepository carRepository,
            IUserRepository userRepository) 
        {
            _logger = logger;
            _userRepository = userRepository;
            _carRepository = carRepository;
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
        public async Task<bool> DeleteAsync(Notification notification)
        {
            try
            {
                await _notificationRepository.DeleteAsync(notification);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }

        public async Task<bool> SendNotificationOnCarIdWithDescription(int carId, Notification notification)
        {
            try
            {
                var users = (await _userRepository.GetUsersAsync())
                    .Where(u => u.Cars
                        .ToList()
                        .Select(c => c.Id)
                        .Contains(carId)).ToList();

                var car = await _carRepository.GetCarByIdAsync(carId);
                if (car == null)
                    throw new Exception("Машина не найдена в бд по id");

                var newNotification = new Notification()
                {
                    IsRead = false,
                    Header = notification.Header,
                    Description = notification.Description
                };
                foreach (var user in users)
                {
                    user.Notifications.Add(newNotification);
                    await _userRepository.UpdateAsync(user);
                }

                //var updatedNotification = await _notificationRepository.CreateAsync(newNotification);
                //if (updatedNotification == null)
                //    throw new Exception("Не удалось добавить уведомление в базу данных");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
