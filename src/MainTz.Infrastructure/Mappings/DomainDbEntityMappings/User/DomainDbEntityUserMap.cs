using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.User
{
    /// <summary>
    /// Конфигурация маппинга для User и UserDto
    /// </summary>
    public class DomainDbEntityUserMap : Profile
    {
        public DomainDbEntityUserMap()
        {
            CreateMap<Application.Models.UserEntities.User, UserEntity>().ReverseMap();
        }
    }
}