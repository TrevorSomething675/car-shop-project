using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels.UserViewModels;
using AutoMapper;

namespace MainTz.Web.Mappings.RequestDomainMappings.User
{
    public class RequestDomainNotificationRequestMap : Profile
    {
        public RequestDomainNotificationRequestMap() 
        {
            CreateMap<NotificationRequest, Notification>().ReverseMap();
        }
    }
}
