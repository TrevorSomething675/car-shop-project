using MainTz.RestApi.DAL.Data.Models.DtoModels;

namespace MainTz.RestApi.BLL.Services.Abstractions
{
	public interface IUsersService
	{
		public Task<List<UserDto>> GetUsers();
		public Task CreateUser(UserDto userDto);
		public Task Delete(UserDto userDto);
	}
}
