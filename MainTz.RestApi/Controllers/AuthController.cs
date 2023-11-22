using MainTz.RestApi.DAL.Data.Models.DtoModels;
using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.dal.Data.Models.Entities;
using Extensions.SettingsModels;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using MainTz.RestApi.DAL.Data.Models.Models;

namespace MainTz.RestApi.Controllers
{
    public class AuthController : Controller
	{
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly AuthApiSettings _authApiSettings;
		private readonly IClientService _clientService;
		private readonly IUsersService _usersService;

		public AuthController(IClientService clientService,
            IHttpContextAccessor contextAccessor, IUsersService usersService)
		{
			_authApiSettings = Settings.Load<AuthApiSettings>("AuthApiSettings");
			_contextAccessor = contextAccessor;
            _clientService = clientService;
			_usersService = usersService;
		}

		[HttpPost]
		public async Task<TokensModel> GetToken(string role) // отправка сообщения и получение токена из AuthApi
		{
			string tokenUrl = $"{_authApiSettings.Url}/{_authApiSettings.GetTokenUrl}";
            TokensModel tokens = await _clientService.SendRequest(tokenUrl, role);

			//if (token == null)
			//	return "Пустой токен";

			return tokens;
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
				var user = await _usersService.GetUserByName(userDto.Name);

				if (user == null)
					return Results.BadRequest("Пользователя не существует");
				if (userDto.Password != user.Password)
					return Results.BadRequest("Неверный пароль");

                var tokens = await GetToken(user.Role.ToString());
				var response = new {
					AccessToken = tokens.AccessToken, 
					RefreshToken = tokens.RefreshToken, 
					Role = user.Role.ToString()
				};

				return Results.Json(response);
			}
			catch (Exception ex)
			{
				return Results.BadRequest($"{ex.Message}");
			}
		}
	}
}