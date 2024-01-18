using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels.UserViewModels;
using AutoMapper;

namespace MainTz.Web.Mappings.RequestDomainMappings.User
{
    public class ResponseDomainNotificationMap : Profile
    {
        public ResponseDomainNotificationMap() 
        {
            CreateMap<Notification, NotificationResponse>().ReverseMap();
        }
    }
}
