namespace MainTz.RestApi.DAL.Data.Models.AuthModels
{
    /// <summary>
    /// Модель для получения access токена, refresh токена и роли
    /// </summary>
    public class TokensModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
    }
}