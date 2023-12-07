using MainTz.RestApi.DAL.Data.Models.AuthModels;
using System.IdentityModel.Tokens.Jwt;
using Extensions.Models.AuthModels;
using MainTz.RestApi.BLL.Services;
using Extensions.SettingsModels;
using Extensions;

namespace MainTz.RestApi.BLL.Middlewares
{
    public class JwtRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthApiSettings _authApiSettings = Settings.Load<AuthApiSettings>("AuthApiSettings");
        public JwtRefreshMiddleware(RequestDelegate next)
        {
            _next = next;
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
                        var refreshTokenModel = new RefreshTokenModel { RefreshToken = refreshToken, Role = role };
                        using var client = new RestClient<RefreshTokenModel, TokensModel>(refreshTokenUrl);
                        var response = await client.GetAsync(refreshTokenModel);

                        context.Response.Cookies.Append("accessToken", response.AccessToken);
                        context.Response.Cookies.Append("refreshToken", response.RefreshToken);
                        context.Response.Cookies.Append("role", response.Role);
                    }
                }
            }

            await _next.Invoke(context);
        }
    }
}