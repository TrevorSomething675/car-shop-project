using MainTz.RestApi.dal.Data.Models.Entities;

namespace MainTz.RestApi.DAL.Repositories.Abstractions
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(int id);
        public Task<List<User>> GetUsers();
        public Task Update(User user);
		public Task Create(User user);
        public Task Delete(User user);
    }
}
