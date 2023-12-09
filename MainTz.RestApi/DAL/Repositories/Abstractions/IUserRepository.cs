using MainTz.RestApi.DAL.Data.Models.Entities;

namespace MainTz.RestApi.DAL.Repositories.Abstractions
{
    public interface IUserRepository
    {
        public Task<User> GetUserByName(string name);
        public Task<List<User>> GetUsers();
        public Task Update(User user);
		public Task Create(User user);
        public Task Delete(User user);
    }
}
