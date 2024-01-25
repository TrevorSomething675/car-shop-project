using MainTz.Application.Models;

namespace MainTz.Application.Services
{
    public interface IUserService
    {
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByNameAsync(string name);
		public Task<User> GetUserByEmailAsync(string email);
        public Task<List<User>> GetSortedUsersByRole();
        public Task<List<User>> GetUsersAsync();
        public Task<User> ChangeRoleForUserByIdAsync(int id);
        public Task<bool> CreateAsync(User user);
        public Task<bool> DeleteAsync(User user);
        public Task<bool> UpdateAsync(User user);
    }
}
