using MainTz.AuthApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.AuthApi.Controllers
{
	public class AuthController : Controller
	{
		private readonly ITokenService _tokenService;
		public AuthController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		public async Task<IActionResult> GetToken()
		{
			try
			{
				var token = _tokenService.CreateAccessToken(Extensions.Roles.User);
				return Ok(token);
			}
			catch
			{
				return BadRequest("Такой роли не существует");
			}
		}
	}
}
