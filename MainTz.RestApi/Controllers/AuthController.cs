using MainTz.RestApi.BLL.Services.Abstractions;
using Extensions.SettingsModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Extensions;

namespace MainTz.RestApi.Controllers
{
    public class AuthController : Controller
	{
		private readonly AuthApiSettings _authApiSettings;
		private readonly IClientService _clientService;
		public AuthController(IClientService clientService)
		{
			_authApiSettings = Settings.Load<AuthApiSettings>("AuthApiSettings");
			_clientService = clientService;
		}

		public async Task<IActionResult> GetToken()
		{
			string tokenUrl = $"{_authApiSettings.Url}/{_authApiSettings.GetTokenUrl}";
			var token = await _clientService.SendRequest(tokenUrl);

			if (token == null)
				return BadRequest("Не удалось получить токен");

			HttpContext.Response.Cookies.Append("token", token);

			return Ok(token);
		}
	}
}