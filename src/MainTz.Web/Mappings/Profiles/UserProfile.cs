using MainTz.Web.ViewModels.AccountViewModels;
using MainTz.Web.ViewModels.UserViewModels;
using MainTz.Application.Models;
using AutoMapper;

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

            CreateMap<UpdatePasswordUserRequest, User>();
            CreateMap<UpdateLoginUserRequest, User>();
        }
    }
}