using AutoMapper;
using MainTz.Application.Models.UserEntities;
using MainTz.Application.Repositories;
using MainTz.Application.Services;
using MainTz.Database.Entities;

namespace MainTz.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public NotificationService(IMapper mapper, INotificationRepository notificationRepository) 
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }
        public async Task<bool> GetNotificationByUser(User user)
        {
            try
            {
                //Доделать!! todo
                var notificationEntity = _mapper.Map<NotificationEntity>(user);
                await _notificationRepository.UpdateNotificationAsync(notificationEntity);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
