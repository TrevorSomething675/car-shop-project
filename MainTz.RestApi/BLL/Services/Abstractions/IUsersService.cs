using MainTz.RestApi.DAL.Data.Models.DtoModels;

namespace MainTz.RestApi.BLL.Services.Abstractions
{
	public interface IUsersService
	{
		public Task<UserDto> GetUserByName(string name);
		public Task<List<UserDto>> GetUsers();
		public Task<bool> Create(UserDto userDto);
		public Task<bool> Delete(UserDto userDto);
		public Task<bool> Update(UserDto userDto);
	}
}