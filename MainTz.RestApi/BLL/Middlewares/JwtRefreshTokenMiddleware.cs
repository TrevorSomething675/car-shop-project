using Extensions.SettingsModels;
using Extensions;

namespace MainTz.RestApi.BLL.Middlewares
{
    public class JwtRefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthApiSettings _authApiSettings;
        public JwtRefreshTokenMiddleware(RequestDelegate next)
        {
            _authApiSettings = Settings.Load<AuthApiSettings>("AuthApiSettings");
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            var requestPath = context.Request.Path;
            var refreshTokensPath = _authApiSettings.GetTokenOnRefreshUrl;

            var token = context.Request.Cookies["Role"];
            var refreshToken = context.Request.Cookies["RefreshToken"];

            if (!string.IsNullOrEmpty(refreshToken) && 
                context.Response.StatusCode == 403 && 
                !string.IsNullOrEmpty(token))
            {
                context.Response.Redirect("/GetTokenByRefreshToken");
            }
        }
    }
}