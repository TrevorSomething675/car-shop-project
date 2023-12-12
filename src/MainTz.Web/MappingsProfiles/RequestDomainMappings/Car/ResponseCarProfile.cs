using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels.UserViewModels;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.RequestDomainMappings.Car
{
    public class ResponseCarProfile : Profile
    {
        public ResponseCarProfile()
        {
            CreateMap<UserResponse, Application.Models.UserEntities.User>();

            CreateMap<Application.Models.UserEntities.User, UserResponse>();
        }
    }
}