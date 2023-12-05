using MainTz.RestApi.DAL.Data.Models.DtoModels;

namespace MainTz.RestApi.BLL.Services.Abstractions
{
	public interface IUsersService
	{
		public Task<UserDto> GetUserByNameAsync(string name);
		public Task<List<UserDto>> GetUsersAsync();
		public Task<bool> CreateAsync(UserDto userDto);
		public Task<bool> DeleteAsync(UserDto userDto);
		public Task<bool> UpdateAsync(UserDto userDto);
	}
}