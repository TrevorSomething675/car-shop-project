using MainTz.Application.Models.AuthModels;

namespace MainTz.Application.Services
{
    public interface ITokenService
    {
        public AuthTokensModel CreateNewTokensModel(string role, string name, int id);
        public bool CheckHealthToken(string token);
    }
}
