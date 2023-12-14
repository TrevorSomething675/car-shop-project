using MainTz.Database.Entities;

namespace MainTz.Application.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserByNameAsync(string name);
		public Task<UserEntity> GetUserByEmailAsync(string email);
		public Task<List<UserEntity>> GetUsers();
        public Task Update(UserEntity user);
        public Task Create(UserEntity user);
        public Task Delete(UserEntity user);
    }
}
