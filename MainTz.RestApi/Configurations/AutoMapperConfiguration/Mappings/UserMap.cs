using MainTz.RestApi.DAL.Data.Models.DtoModels;
using MainTz.RestApi.dal.Data.Models.Entities;
using AutoMapper;

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
