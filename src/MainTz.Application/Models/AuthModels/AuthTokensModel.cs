namespace MainTz.Application.Models.AuthModels
{
    public class AuthTokensModel
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}
