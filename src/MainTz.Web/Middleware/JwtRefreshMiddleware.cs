using MainTz.Application.Services;

namespace MainTz.Web.Middleware
{
    public class JwtRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _contextAccessor;
        public JwtRefreshMiddleware(RequestDelegate next, ITokenService tokenService, 
            IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _tokenService = tokenService;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Cookies["accessToken"]))
            {
                var accessToken = context.Request.Cookies["accessToken"];
                var refreshToken = context.Request.Cookies["refreshToken"];
                var accessTokenIsValid = _tokenService.CheckHealthToken(accessToken);
                var refreshTokenIsValid = _tokenService.CheckHealthToken(refreshToken);

                if (accessTokenIsValid && refreshTokenIsValid)
                {
					var role = context.Request.Cookies["role"];
                    var userId = Convert.ToInt32(context.Request.Cookies["userId"]);
                    var userName = context.Request.Cookies["userName"];
                    var newAuthTokensModel = _tokenService.CreateNewTokensModel(role, userName, userId);

                    context.Response.Cookies.Append("userName", newAuthTokensModel.UserName);
                    context.Response.Cookies.Append("accessToken", newAuthTokensModel.AccessToken);
                    context.Response.Cookies.Append("refreshToken", newAuthTokensModel.RefreshToken);
                    context.Response.Cookies.Append("role", newAuthTokensModel.Role);
                    context.Response.Cookies.Append("id", newAuthTokensModel.UserId.ToString());
                }
            }

            await _next.Invoke(context);
        }
    }
}