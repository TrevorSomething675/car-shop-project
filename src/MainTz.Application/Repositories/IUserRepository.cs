using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByNameAsync(string name);
		public Task<User> GetUserByEmailAsync(string email);
        public Task RemoveCarFromUser(User user, Car car);
		public Task<List<User>> GetUsersAsync();
        public Task UpdateAsync(User user);
        public Task CreateAsync(User user);
        public Task DeleteAsync(User user);
    }
}
