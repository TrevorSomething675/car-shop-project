namespace MainTz.Application.Models.OptionsModels
{
    public class JwtAuthOptions
    {
        public const string SectionName = "JwtAuthOptions";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
