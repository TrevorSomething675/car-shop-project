namespace MainTz.Application.Models.SittingsModels
{
    public class JwtAuthSettings
    {
        public const string JwtAuthPosition = "JwtAuthSettings";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
