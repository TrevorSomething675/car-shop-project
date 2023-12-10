namespace MainTz.Application.Services
{
    public interface IUserService
    {
        public Task<UserDto> GetUserByNameAsync(string name);
        public Task<List<UserDto>> GetUsersAsync();
        public Task<bool> CreateAsync(UserDto userDto);
        public Task<bool> DeleteAsync(UserDto userDto);
        public Task<bool> UpdateAsync(UserDto userDto);
    }
}
