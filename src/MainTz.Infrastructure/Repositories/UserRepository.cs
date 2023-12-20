using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using System.Xml.Linq;

namespace MainTz.Infrastructure.Repositories
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

        public async Task<UserEntity> GetUserByNameAsync(string name)
        {
            var user = await _mainContext.Users
                .Include(user => user.Role)
                .Include(user => user.Cars)
                .Include(user => user.Notifications)
                .FirstOrDefaultAsync(user => user.Name == name);
            return user;
        }
		public async Task<UserEntity> GetUserByEmailAsync(string email)
		{
			var user = await _mainContext.Users
	            .Include(user => user.Role)
	            .Include(user => user.Cars)
	            .FirstOrDefaultAsync(user => user.Email == email);
			return user;
		}
		public async Task<List<UserEntity>> GetUsers()
        {
            var users = await _mainContext.Users.ToListAsync();
            return users;
        }

        public async Task Update(UserEntity user)
        {
            _mainContext.Users.Update(user);
            await _mainContext.SaveChangesAsync();
        }

        public async Task Create(UserEntity user)
        {
            _mainContext.Users.Add(user);
            await _mainContext.SaveChangesAsync();
        }

        public async Task Delete(UserEntity user)
        {
            _mainContext.Users.Remove(user);
            await _mainContext.SaveChangesAsync();
        }
	}
}
