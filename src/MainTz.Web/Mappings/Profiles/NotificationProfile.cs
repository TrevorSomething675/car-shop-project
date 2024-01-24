using MainTz.Web.ViewModels.NotificationViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationRequest, Notification>();
            CreateMap<Notification, NotificationResponse>();

            CreateMap<NotificationRequest, NotificationResponse>();
        }
    }
}
