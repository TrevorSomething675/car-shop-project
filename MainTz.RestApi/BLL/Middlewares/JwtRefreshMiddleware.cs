using MainTz.RestApi.BLL.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using Extensions.SettingsModels;
using Extensions;

namespace MainTz.RestApi.BLL.Middlewares
{
    public class JwtRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IClientService _clientService;
        private readonly AuthApiSettings _authApiSettings = Settings.Load<AuthApiSettings>("AuthApiSettings");
        public JwtRefreshMiddleware(RequestDelegate next, IClientService clientService)
        {
            _next = next;
            _clientService = clientService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Cookies["accessToken"]))
            {
                var accessToken = context.Request.Cookies["accessToken"];
                var handler = new JwtSecurityTokenHandler();
                var jwtTokenAccessToken = handler.ReadJwtToken(accessToken);
                var accessTokenValidTo = jwtTokenAccessToken.ValidTo;

                if(accessTokenValidTo > DateTime.Now)
                {
                    var refreshToken = context.Request.Cookies["refreshToken"];
                    var role = context.Request.Cookies["role"];
                    var refreshTokenUrl = $"{_authApiSettings.Url}/{_authApiSettings.GetTokenOnRefreshUrl}";

                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        var tokens = await _clientService.SendRequestWithTokenAsync(refreshTokenUrl, role, refreshToken);
                        context.Response.Cookies.Append("accessToken", tokens.AccessToken);
                        context.Response.Cookies.Append("refreshToken", tokens.RefreshToken);
                        context.Response.Cookies.Append("role", tokens.Role);
                    }
                }
            }

            await _next.Invoke(context);
        }
    }
}