namespace MainTz.Application.Models.OptionsModels
{
    public class JwtAuthOptions
    {
        public const string JwtAuthPosition = "JwtAuthOptions";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
