namespace Extensions.SettingsModels
{
	public class AuthSettings
	{
		public string SecretKey { get; private set; } = null!;
		public string? Audience { get; private set; }
		public string? Issuer { get; private set; }
	}
}
