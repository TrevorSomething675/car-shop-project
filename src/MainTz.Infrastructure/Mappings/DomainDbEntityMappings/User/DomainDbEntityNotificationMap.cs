using MainTz.Application.Models.UserEntities;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.User
{
    public class DomainDbEntityNotificationMap : Profile
    {
        public DomainDbEntityNotificationMap()
        {
            CreateMap<Notification, NotificationEntity>().ReverseMap();
        }
    }
}
