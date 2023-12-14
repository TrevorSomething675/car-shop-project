using MainTz.Web.ViewModels.UserViewModels;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.RequestDomainMappings.User
{
    public class ResponseDomainUserMap : Profile
    {
        public ResponseDomainUserMap()
        {
            CreateMap<Application.Models.UserEntities.User, UserResponse>();
        }
    }
}