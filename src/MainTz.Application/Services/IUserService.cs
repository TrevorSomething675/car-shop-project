using MainTz.Application.Models;
using MainTz.Core.Enums;

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
        public Task<bool> UpdateUserData(User user, int userId);
    }
}