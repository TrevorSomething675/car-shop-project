using MainTz.RestApi.DAL.Data.Models.DtoModels;
using MainTz.RestApi.BLL.Services.Abstractions;
using Extensions.SettingsModels;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MainTz.RestApi.dal.Data.Models.Entities;
using System.Runtime.CompilerServices;

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
        public async Task<IActionResult> Login(UserDto userDto)
		{
			try
			{
				//var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

				//if (user == null)
				//	return BadRequest("Пользователь не зарегистрирован");

				var token = await GetToken(userDto.Role.ToString());
				//user.AccessToken = token;
				
				//await _userManager.UpdateAsync(user);
				//_signInManager.SignInAsync(user);

				return Ok(token);
            }
			catch(Exception ex)
			{
				return BadRequest($"{ex.Message}");
            }

        }
	}
}