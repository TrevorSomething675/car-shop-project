using MainTz.Database.Entities;

namespace MainTz.Application.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserByNameAsync(string name);
		public Task<UserEntity> GetUserByEmailAsync(string email);
		public Task<List<UserEntity>> GetUsersAsync();
        public Task UpdateAsync(UserEntity user);
        public Task CreateAsync(UserEntity user);
        public Task DeleteAsync(UserEntity user);
    }
}
