using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            var userEntity = await _userRepository.GetUserByIdAsync(id);
            var user = _mapper.Map<User>(userEntity);
            return user;
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
            var usersEntity = await _userRepository.GetUsersAsync();
            var usersDomainEntity = _mapper.Map<List<User>>(usersEntity);
            return usersDomainEntity;
        }
        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                await _userRepository.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                user.Role = await _roleRepository.GetRoleByNameAsync("User");
				await _userRepository.CreateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteAsync(User user)
        {
            try
            {
                await _userRepository.DeleteAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<List<User>> GetSortedUsersByRole()
        {
            var users = await _userRepository.GetUsersAsync();
            var sortedUsers = users.OrderBy(u => u.Role.Name).ToList();
            return sortedUsers;
        }

        public async Task<User> ChangeRoleForUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user.Role.Name == "User")
                user.Role = await _roleRepository.GetRoleByNameAsync("Manager");
            else if (user.Role.Name == "Manager")
                user.Role = await _roleRepository.GetRoleByNameAsync("User");

            var updatedUser = await _userRepository.UpdateAsync(user);
            return updatedUser;
        }
    }
}