using System.Text;

namespace Extensions.SettingsModels
{
	public class AuthSettings
	{
		public string? Key { get; private set; }
		public string? Audience { get; private set; }
		public string? Issuer { get; private set; }
		public int AccessTokenExp { get; private set; }
		public int RefreshTokenExp { get; private set; }
	}
}
