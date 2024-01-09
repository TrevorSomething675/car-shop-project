using MainTz.Web.ViewModels;
using AutoMapper;

namespace MainTz.Web.Mappings.RequestDomainMappings.User
{
    public class RequestRegisterUserMap : Profile
    {
        public RequestRegisterUserMap()
        {
            CreateMap<RegisterFormRequest, Application.Models.UserEntities.User>();
            CreateMap<Application.Models.UserEntities.User, RegisterFormRequest>();
        }
    }
}