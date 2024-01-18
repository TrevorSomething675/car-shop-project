using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterFormRequest, User>();
            CreateMap<User, RegisterFormRequest>();
        }
    }
}