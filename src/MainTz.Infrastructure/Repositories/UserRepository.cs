using MainTz.Application.Models.UserEntities;
using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using AutoMapper;

namespace MainTz.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий UserRepository это обёртка на MainContext для таблицы User
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;
        public UserRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }
        public async Task<User> GetUserByNameAsync(string name)
        {
            await using (var context = _dbContextFactory.CreateDbContext())
            {
                var userEntity = await context.Users
                    .Include(user => user.Role)
                    .Include(user => user.Cars)
                    .Include(user => user.Notifications)
                    .FirstOrDefaultAsync(user => user.Name == name);
                var user = _mapper.Map<User>(userEntity);
                return user;
            }
        }
		public async Task<User> GetUserByEmailAsync(string email)
		{
            await using(var context = _dbContextFactory.CreateDbContext())
            {
			    var userEntity = await context.Users
	                .Include(user => user.Role)
	                .Include(user => user.Cars)
	                .FirstOrDefaultAsync(user => user.Email == email);
                var user = _mapper.Map<User>(userEntity);
                return user;
            }
		}
		public async Task<List<User>> GetUsersAsync()
        {
            await using (var context = _dbContextFactory.CreateDbContext())
            {
                var userEntities = await context.Users.ToListAsync();
                var users = _mapper.Map<List<User>>(userEntities);
                return users;
            }
        }

        public async Task UpdateAsync(User user)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                //var updatedUserEntity = _mapper.Map<UserEntity>(user);
                //var userEntity = await context.Users
                //    .Include(u => u.Cars)
                //    .FirstAsync(u => u.Id == updatedUserEntity.Id);

                //foreach (var carName in updatedUserEntity.Cars.Select(c => c.Name))
                //{
                //    if (!userEntity.Cars.Select(c => c.Name).ToList().Contains(carName))
                //        userEntity.Cars.Add(updatedUserEntity.Cars.FirstOrDefault(car => car.Name == carName)!);
                //}

                //_mapper.Map(updatedUserEntity, userEntity);
                //context.Cars.AttachRange(userEntity.Cars);
                //await context.SaveChangesAsync();

                var userEntity = _mapper.Map<UserEntity>(user);
                context.Attach(userEntity);
                context.Update(userEntity);
                await context.SaveChangesAsync();
            }
        }
        public async Task CreateAsync(User user)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var userEntity = _mapper.Map<UserEntity>(user);
                context.Users.Add(userEntity);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(User user)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var userEntity = _mapper.Map<UserEntity>(user);
                context.Users.Remove(userEntity);
                await context.SaveChangesAsync();
            }
        }
	}
}