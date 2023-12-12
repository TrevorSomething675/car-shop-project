using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels.UserViewModels;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.RequestDomainMappings.User
{
    public class RequestDomainUserMap : Profile
    {
        public RequestDomainUserMap()
        {
            CreateMap<Application.Models.UserEntities.User, UserRequest>();
        }
    }
}