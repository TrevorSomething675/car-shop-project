using MainTz.RestApi.DAL.Repositories.Abstractions;
using MainTz.RestApi.dal.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainTz.RestApi.DAL.Repositories
{
	/// <summary>
	/// Репозиторий UserRepository это обёртка на MainContext для таблицы User
	/// </summary>
	public class UserRepository : IUserRepository
    {
        private readonly MainContext _mainContext;
        public UserRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public async Task<User> GetUserByName(string name)
		{
            var user = await _mainContext.Users
                .FirstOrDefaultAsync(user=>user.Name == name);
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _mainContext.Users.ToListAsync();
            return users;
        }

        public async Task Update(User user)
        {
            _mainContext.Users.Update(user);
            await _mainContext.SaveChangesAsync();
        }

        public async Task Create(User user)
        {
            _mainContext.Users.Add(user);
            await _mainContext.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _mainContext.Users.Remove(user);
            await _mainContext.SaveChangesAsync();
        }
    }
}