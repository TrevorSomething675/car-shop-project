using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByNameAsync(string name);
		public Task<User> GetUserByEmailAsync(string email);
		public Task<List<User>> GetUsersAsync();

        public Task<User> UpdateAsync(User user);
        public Task CreateAsync(User user);
        public Task DeleteAsync(User user);
    }
}
