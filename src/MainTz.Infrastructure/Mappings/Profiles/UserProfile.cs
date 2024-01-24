using MainTz.Database.Entities;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserEntity>().ReverseMap();
        }
    }
}