namespace MainTz.AuthApi.Services.Abstractions
{
    public interface ITokenService
	{
        public string CreateAccessToken(string roles);
        public string CreateRefreshToken(string roles);
    }
}
