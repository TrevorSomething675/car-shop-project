using MainTz.Database.Entities;
using MainTz.Web.ViewModels;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings
{
    /// <summary>
    /// Конфигурация маппинга для User и UserDto
    /// </summary>
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserEntity, LoginFormRequest>();

            CreateMap<LoginFormRequest, UserEntity>();
        }
    }
}
