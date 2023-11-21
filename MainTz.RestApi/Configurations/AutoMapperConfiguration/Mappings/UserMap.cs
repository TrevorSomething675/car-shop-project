using MainTz.RestApi.DAL.Data.Models.DtoModels;
using MainTz.RestApi.dal.Data.Models.Entities;
using AutoMapper;

namespace MainTz.RestApi.Configurations.AutoMapperConfiguration.Mappings
{
	public class UserMap : Profile
	{
		public UserMap() 
		{
			CreateMap<User, UserDto>().
				ForMember(user => user.Id, opt => opt.Ignore()).
				ForMember(user => user.Role, opt => opt.Ignore()).ReverseMap();
		}
	}
}
