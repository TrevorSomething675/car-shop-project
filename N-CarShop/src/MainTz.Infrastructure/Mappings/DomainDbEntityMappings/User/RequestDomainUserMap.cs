using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.User
{
    public class RequestDomainUserMap : Profile
    {
        public RequestDomainUserMap()
        {
            CreateMap<UserDomainEntity, UserRequest>();
        }
    }
}
