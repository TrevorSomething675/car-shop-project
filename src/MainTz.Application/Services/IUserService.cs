using MainTz.Application.Models.UserEntities;

namespace MainTz.Application.Services
{
    public interface IUserService
    {
        public Task<User> GetUserByNameAsync(string name);
        public Task<List<User>> GetUsersAsync();
        public Task<bool> CreateAsync(User user);
        public Task<bool> DeleteAsync(User user);
        public Task<bool> UpdateAsync(User user);
    }
}
