using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MainTz.Application.Models;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using AutoMapper;
using System.Diagnostics.Contracts;

namespace MainTz.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий UserRepository это обёртка на MainContext для таблицы User
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        public UserRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper,
            ILogger<UserRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<User> GetUserByNameAsync(string name)
        {
            await using (var context = _dbContextFactory.CreateDbContext())
            {
                var userEntity = await context.Users
                    .Include(user => user.Role)
                    .Include(user => user.Cars)
                    .ThenInclude(car => car.Images)
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
                var userEntities = await context.Users
                    .Include(u => u.Notifications)
                    .Include(u => u.Cars)
                    .ToListAsync();

                var users = _mapper.Map<List<User>>(userEntities);
                return users;
            }
        }
        public async Task UpdateAsync(User user)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var updatedUserEntity = _mapper.Map<UserEntity>(user);
                var userEntity = await context.Users
                    .Include(u => u.Cars)
                    .Include(u => u.Notifications)
                    .FirstOrDefaultAsync(u => u.Id == updatedUserEntity.Id);

                userEntity.Name = updatedUserEntity.Name;
                userEntity.Password = updatedUserEntity.Password;

                if(user.Notifications != null)
                {
                    if(userEntity.Notifications.Count() < updatedUserEntity.Notifications.Count) 
                    {
                        foreach (var notification in userEntity.Notifications)
                        {
                            var notificationToRemove = updatedUserEntity.Notifications.FirstOrDefault(n => n.Id == notification.Id);
                            updatedUserEntity.Notifications.Remove(notificationToRemove);
                        }
                        userEntity.Notifications.AddRange(updatedUserEntity.Notifications);
                    }
                }
                if (user.Cars != null)
                {
                    if (userEntity.Cars.Count() < updatedUserEntity.Cars.Count())
                    {
                        foreach (var car in userEntity.Cars)
                        {
                            var carToRemove = updatedUserEntity.Cars.FirstOrDefault(c => c.Id == car.Id);
                            updatedUserEntity.Cars.Remove(carToRemove);
                        }
                        userEntity.Cars.AddRange(updatedUserEntity.Cars);
                    }
                    else if (userEntity.Cars.Count() > updatedUserEntity.Cars.Count())
                    {
                        var userEntitiesId = userEntity.Cars.Select(c => c.Id).ToList();

                        foreach (var carId in userEntitiesId)
                        {
                            var carToRemove = updatedUserEntity.Cars.FirstOrDefault(c => c.Id == carId);
                            if(carToRemove == null)
                            {
                                var carEntityToRemove = userEntity.Cars.First(c => c.Id == carId);
                                userEntity.Cars.Remove(carEntityToRemove);
                            }
                        }
                    }
                }
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
        public async Task RemoveCarFromUser(User userModel, Car carModel)
        {
            await using(var context = _dbContextFactory.CreateDbContext())
            {
                var userEntity = context.Users
                    .Include(u => u.Cars)
                    .FirstOrDefault(user => user.Name == userModel.Name);
                var carEntity = context.Cars.FirstOrDefault(car => car.Name == carModel.Name);

                userEntity.Cars.Remove(carEntity);
                await context.SaveChangesAsync();
            }
        }
    }
}