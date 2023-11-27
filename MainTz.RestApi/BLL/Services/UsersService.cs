using MainTz.RestApi.DAL.Repositories.Abstractions;
using MainTz.RestApi.DAL.Data.Models.DtoModels;
using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.dal.Data.Models.Entities;
using AutoMapper;

namespace MainTz.RestApi.BLL.Services
{
	public class UsersService : IUsersService
	{
		private readonly IUserRepository _userRepository;
		private readonly ILogger<UsersService> _logger;
		private readonly IMapper _mapper;

		public UsersService(IUserRepository userRepository, ILogger<UsersService> logger, IMapper mapper) 
		{
			_userRepository = userRepository;
			_logger = logger;
			_mapper = mapper;
		}
		public async Task<UserDto> GetUserByName(string name)
		{
			var user = await _userRepository.GetUserByName(name);
			var userDto = _mapper.Map<UserDto>(user);

			return userDto;
		}

		public async Task<List<UserDto>> GetUsers()
		{
			var users = await _userRepository.GetUsers();
			var usersDto = _mapper.Map<List<UserDto>>(users);

			return usersDto;
		}

		public async Task<bool> Update(UserDto userDto)
		{
			try
			{
				var user = _mapper.Map<User>(userDto);
				await _userRepository.Update(user);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"{ex.Message}");
				return false;
			}
		}

		public async Task<bool> Create(UserDto userDto)
		{
			try
			{
				var user = _mapper.Map<User>(userDto);
				await _userRepository.Create(user);
				return true;
			}
			catch(Exception ex)
			{
				_logger.LogInformation($"{ex.Message}");
				return false;
			}
		}

		public async Task<bool> Delete(UserDto userDto)
		{
			try
			{
				var user = _mapper.Map<User>(userDto);
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
