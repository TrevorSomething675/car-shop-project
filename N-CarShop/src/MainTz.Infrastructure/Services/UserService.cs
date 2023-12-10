using AutoMapper;
using MainTz.Application.Repositories;
using MainTz.Application.Services;
using MainTz.Database.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTz.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserDto> GetUserByNameAsync(string name)
        {
            var user = await _userRepository.GetUserByName(name);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return usersDto;
        }

        public async Task<bool> UpdateAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(userDto);
                await _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(userDto);
                await _userRepository.Create(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(userDto);
                await _userRepository.Delete(user);
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
