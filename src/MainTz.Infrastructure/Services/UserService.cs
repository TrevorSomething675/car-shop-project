using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using MainTz.Database.Entities;
using AutoMapper;
using MainTz.Application.Models.UserEntities;

namespace MainTz.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<User> GetUserByNameAsync(string name)
        {
            var userEntity = await _userRepository.GetUserByNameAsync(name);
            var userDomainEntity = _mapper.Map<User>(userEntity);
            return userDomainEntity;
        }
		public async Task<User> GetUserByEmailAsync(string email)
		{
			var userEntity = await _userRepository.GetUserByEmailAsync(email);
			var userDomainEntity = _mapper.Map<User>(userEntity);
			return userDomainEntity;
		}
		public async Task<List<User>> GetUsersAsync()
        {
            var usersEntity = await _userRepository.GetUsers();
            var usersDomainEntity = _mapper.Map<List<User>>(usersEntity);
            return usersDomainEntity;
        }
        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                var userEntity = _mapper.Map<UserEntity>(user);
                await _userRepository.Update(userEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                var userEntity = _mapper.Map<UserEntity>(user);
                userEntity.Role = await _roleRepository.GetRoleByNameAsync("User");
				await _userRepository.Create(userEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(User user)
        {
            try
            {
                var userEntity = _mapper.Map<UserEntity>(user);
                await _userRepository.Delete(userEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
    }
}
