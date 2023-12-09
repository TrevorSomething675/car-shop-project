using MainTz.RestApi.DAL.Data.Models.DtoModels;
using AutoMapper;
using MainTz.RestApi.DAL.Data.Models.Entities;

namespace MainTz.RestApi.Configurations.AutoMapperConfiguration.Mappings
{
	/// <summary>
	/// Конфигурация маппинга для User и UserDto
	/// </summary>
	public class UserMap : Profile
	{
		public UserMap() 
		{
			CreateMap<User, UserDto>();

			CreateMap<UserDto, User>();
		}
	}
}
