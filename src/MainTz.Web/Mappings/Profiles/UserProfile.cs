using MainTz.Web.ViewModels.UserViewModels;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Web.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();

            CreateMap<RegisterUserRequest, User>();

            CreateMap<LoginUserRequest, User>();
        }
    }
}