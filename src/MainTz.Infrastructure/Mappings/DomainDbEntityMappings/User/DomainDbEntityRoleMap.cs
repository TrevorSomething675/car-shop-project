using MainTz.Application.Models.UserEntities;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.User
{
	public class DomainDbEntityRoleMap : Profile
	{
		public DomainDbEntityRoleMap()
		{
			CreateMap<Role, RoleEntity>().ReverseMap();
		}
	}
}