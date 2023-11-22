using Extensions;

namespace MainTz.AuthApi.Services.Abstractions
{
    public interface ITokenService
	{
        public string CreateAccessToken(Roles roles);
        public string CreateRefreshToken(Roles roles);
    }
}
