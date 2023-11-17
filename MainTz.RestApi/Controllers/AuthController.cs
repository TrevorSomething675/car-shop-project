using MainTz.RestApi.DAL.Data.Models.DtoModels;
using MainTz.RestApi.BLL.Services.Abstractions;
using Extensions.SettingsModels;
using Microsoft.AspNetCore.Mvc;
using Extensions;

namespace MainTz.RestApi.Controllers
{
    public class AuthController : Controller
	{
		private readonly AuthApiSettings _authApiSettings;
		private readonly IClientService _clientService;
		private readonly IUsersService _usersService;
		public AuthController(IClientService clientService, IUsersService usersService)
		{
			_authApiSettings = Settings.Load<AuthApiSettings>("AuthApiSettings");
            _clientService = clientService;
			_usersService = usersService;
		}

		

		[HttpPost]
		public async Task<string> GetToken(string role) // отправка сообщения и получение токена из AuthApi
		{
			string tokenUrl = $"{_authApiSettings.Url}/{_authApiSettings.GetTokenUrl}";
			var token = await _clientService.SendRequest(tokenUrl, role);

			if (token == null)
				return "Пустой токен";

			return token;
		}

		[HttpGet]
        public async Task<IActionResult> Login()
        {
			return View();
        }

        [HttpPost]
        public async Task<IResult> Login(UserDto userDto)
		{
			try
			{
				await _usersService.CreateUser(userDto);
				var token = await GetToken(userDto.Role.ToString());
				return Results.Json(token);
            }
			catch(Exception ex)
			{
				return Results.Json($"{ex.Message}");
            }

        }
	}
}