using MainTz.RestApi.DAL.Data.Models.DtoModels;
using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.dal.Data.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Extensions.SettingsModels;
using Microsoft.AspNetCore.Mvc;
using Extensions;

namespace MainTz.RestApi.Controllers
{
    public class AuthController : Controller
	{
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly AuthApiSettings _authApiSettings;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly IClientService _clientService;
		public AuthController(IClientService clientService, UserManager<User> userManager,
            IHttpContextAccessor contextAccessor, SignInManager<User> signInManager)
		{
			_authApiSettings = Settings.Load<AuthApiSettings>("AuthApiSettings");
			_contextAccessor = contextAccessor;
            _clientService = clientService;
			_signInManager = signInManager;
			_userManager = userManager;
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
				var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

				//if (user == null)
				//	return BadRequest("Пользователь не зарегистрирован");

				var token = await GetToken(userDto.Role.ToString());
				//user.AccessToken = token;
				
				//await _userManager.UpdateAsync(user);
				//_signInManager.SignInAsync(user);

				return Results.Json(token);
            }
			catch(Exception ex)
			{
				return Results.Json($"{ex.Message}");
            }

        }
	}
}