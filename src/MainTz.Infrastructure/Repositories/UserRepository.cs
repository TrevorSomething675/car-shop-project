using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;

namespace MainTz.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий UserRepository это обёртка на MainContext для таблицы User
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        public UserRepository(IDbContextFactory<MainContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<UserEntity> GetUserByNameAsync(string name)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = await context.Users
                    .Include(user => user.Role)
                    .Include(user => user.Cars)
                    .Include(user => user.Notifications)
                    .FirstOrDefaultAsync(user => user.Name == name);
                return user;
            }
        }
		public async Task<UserEntity> GetUserByEmailAsync(string email)
		{
            using(var context = _dbContextFactory.CreateDbContext())
            {
			    var user = await context.Users
	                .Include(user => user.Role)
	                .Include(user => user.Cars)
	                .FirstOrDefaultAsync(user => user.Email == email);
			    return user;
            }
		}
		public async Task<List<UserEntity>> GetUsersAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var users = await context.Users.ToListAsync();
                return users;
            }
        }

        public async Task UpdateAsync(UserEntity user)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(UserEntity user)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(UserEntity user)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
	}
}
