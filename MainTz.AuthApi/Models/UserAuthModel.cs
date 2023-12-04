namespace MainTz.AuthApi.Models
{
    public class UserAuthModel
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpTime { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
    }
}