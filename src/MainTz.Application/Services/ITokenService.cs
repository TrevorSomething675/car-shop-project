namespace MainTz.Application.Services
{
    public interface ITokenService
    {
        public string CreateAccessToken(string role);
        public string CreateRefreshToken(string role);
    }
}
