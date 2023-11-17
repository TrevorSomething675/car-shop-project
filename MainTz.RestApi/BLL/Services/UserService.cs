using MainTz.RestApi.DAL.Repositories.Abstractions;
using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.dal.Data.Models.Entities;
using MainTz.RestApi.DAL.Data.Models.DtoModels;
using AutoMapper;

namespace MainTz.RestApi.BLL.Services
{
	public class UserService : IUsersService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public UserService(IUserRepository userRepository, ILogger logger, IMapper mapper) 
		{
			_logger = logger;
			_mapper = mapper;
			_userRepository = userRepository;
		}

		public async Task<List<UserDto>> GetUsers()
		{
			var users = await _userRepository.GetUsers();
			var usersDto = _mapper.Map<List<UserDto>>(users);

			return usersDto;
		}

		public async Task Update(UserDto userDto)
		{
			try
			{
				var user = _mapper.Map<User>(userDto);
				await _userRepository.Update(user);
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"{ex.Message}");
			}
		}

		public async Task CreateUser(UserDto userDto)
		{
			try
			{
				var user = _mapper.Map<User>(userDto);
				await _userRepository.Create(user);
			}
			catch(Exception ex)
			{
				_logger.LogInformation($"{ex.Message}");
			}
		}

		public async Task Delete(UserDto userDto)
		{
			try
			{
				var user = _mapper.Map<User>(userDto);
				await _userRepository.Delete(user);
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"{ex.Message}");
			}
		}
	}
}
