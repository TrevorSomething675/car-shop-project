namespace MainTz.Application.Services
{
    public interface ITokenService
    {
        public string CreateAccessToken(string role, string name);
        public string CreateRefreshToken(string role, string name);
    }
}
