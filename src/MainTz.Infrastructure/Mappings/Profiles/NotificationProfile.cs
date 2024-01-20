using MainTz.Database.Entities;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Mappings.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationEntity>().ReverseMap();
        }
    }
}