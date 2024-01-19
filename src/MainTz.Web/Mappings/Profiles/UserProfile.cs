using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Web.ViewModels;
using AutoMapper;

namespace MainTz.Web.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, User>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();

            CreateMap<RegisterUserRequest, User>();

            CreateMap<LoginUserRequest, User>();
        }
    }
}