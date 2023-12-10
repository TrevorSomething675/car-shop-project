using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Application.Models;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car
{
    public class RequestDomainCarMap : Profile
    {
        public RequestDomainCarMap() 
        {
            CreateMap<UserResponse, UserDomainEntity>();
        }
    }
}
