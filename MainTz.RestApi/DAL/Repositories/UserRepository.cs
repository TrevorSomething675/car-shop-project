using MainTz.RestApi.DAL.Repositories.Abstractions;
using MainTz.RestApi.dal.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainTz.RestApi.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainContext _mainContext;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(MainContext mainContext, ILogger<UserRepository> logger)
        {
            _logger = logger;
            _mainContext = mainContext;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _mainContext.Users
                .FirstOrDefaultAsync(user=>user.Id == id);
            return user;

        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _mainContext.Users.ToListAsync();
            return users;
        }

        public async Task Update(User user)
        {
            try
            {
                _mainContext.Users.Update(user);
                await _mainContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
            }
        }

        public async Task Create(User user)
        {
            try
            {
                _mainContext.Users.Add(user);
                await _mainContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
            }
        }

        public async Task Delete(User user)
        {
            try
            {
                _mainContext.Users.Remove(user);
                await _mainContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
            }
        }
    }
}