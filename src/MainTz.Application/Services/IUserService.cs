using MainTz.Application.Models.UserEntities;

namespace MainTz.Application.Services
{
    public interface IUserService
    {
        public Task<User> GetUserByNameAsync(string name);
        public Task<List<User>> GetUsersAsync();
        public Task<bool> CreateAsync(User userDto);
        public Task<bool> DeleteAsync(User userDto);
        public Task<bool> UpdateAsync(User userDto);
    }
}
