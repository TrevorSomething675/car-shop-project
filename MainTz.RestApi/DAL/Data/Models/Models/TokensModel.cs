using Extensions;

namespace MainTz.RestApi.DAL.Data.Models.Models
{
    /*Модель для получения роли, accessToken и refreshToken*/
    public class TokensModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
    }
}