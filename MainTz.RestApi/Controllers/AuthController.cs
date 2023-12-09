using MainTz.RestApi.DAL.Data.Models.AuthModels;
using MainTz.RestApi.DAL.Data.Models.DtoModels;
using MainTz.RestApi.BLL.Services.Abstractions;
using Extensions.Models.AuthModels;
using MainTz.RestApi.BLL.Services;
using Extensions.SettingsModels;
using Microsoft.AspNetCore.Mvc;
using Extensions;

namespace MainTz.RestApi.Controllers
{
    public class AuthController : Controller
	{
		private readonly AuthApiSettings _authApiSettings = Settings.Load<AuthApiSettings>("AuthApiSettings");
		private readonly IUsersService _usersService;
		private readonly ILogger<AuthController> _logger;
		public AuthController( IUsersService usersService, ILogger<AuthController> logger)
		{
			_usersService = usersService;
			_logger = logger;
		}
		/// <summary>
		/// Получение токена из сервиса, путём отправки запроса с ролью
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
        [HttpPost]
		public async Task<IResult> GetToken([FromBody]string role) // отправка сообщения и получение токена из AuthApi
		{
			string tokenUrl = $"{_authApiSettings.Url}/{_authApiSettings.GetTokenUrl}";
			var refreshTokenModel = new RefreshTokenModel { Role = role};

			using var client = new RestClient<RefreshTokenModel, TokensModel>(tokenUrl);
			var tokens = await client.GetAsync(refreshTokenModel);

			return Results.Json(tokens);
		}
		/// <summary>
		/// Получение разметки с формой логина
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public async Task<IActionResult> Login()
        {
			_logger.LogDebug("Login debug");
			_logger.LogTrace("Login trace");
			return View();
        }
		/// <summary>
		/// Отправка формы логина, она возрвращает json, js код в FormScripts.js 
		/// получает её и в зависимости от роли перенаправляет пользователя на страницу роли.
		/// </summary>
		/// <param name="userDto">Модель, которая приходит с фронта</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IResult> Login(UserDto userDto)
		{
			if (!ModelState.IsValid)
				return Results.BadRequest("Wrong Data");
            try
			{
				var user = await _usersService.GetUserByNameAsync(userDto.Name);

				if (user == null)
					return Results.BadRequest("Пользователя не существует");
				if (userDto.Password != user.Password)
					return Results.BadRequest("Неверный пароль");

                var tokens = await GetToken(user.Role.ToString());

				return Results.Json(tokens);
			}
			catch (Exception ex)
			{
				return Results.BadRequest($"{ex.Message}");
			}
		}
		/// <summary>
		/// Получение разметки с формой регистрации
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
		/// <summary>
		/// Регистрация пользователя, по умолчанию роль всегда 'User', 
		/// мы регистрируем пользователя, а после вызываем метод Login, результат обрабатывает RegisterFormScript.js
		/// и перенаправляет пользователя на нужную страницу после регистрации.
		/// </summary>
		/// <param name="userDto"></param>
		/// <returns></returns>
		[HttpPost]
        public async Task<IResult> Register(UserDto userDto)
        {
			if (!ModelState.IsValid)
				return Results.BadRequest("Wrong Data");

            var user = await _usersService.GetUserByNameAsync(userDto.Name);

            if (user != null)
                return Results.BadRequest("Пользователь уже существует");

			userDto.Role = "User";
            await _usersService.CreateAsync(userDto);

			var result = await Login(userDto);

			return result;
        }
    }
}