namespace Extensions.SettingsModels
{
	/// <summary>
	/// Модель для получения конфигурациии токена и урла сервиса аутентификации. То есть по 
	/// какому урлу будет отправлен запрос из RestApi для получения токена
	/// </summary>
	public class AuthApiSettings
	{
		public string Url { get; set; }
		public string GetTokenUrl { get; set; }
		public string GetTokenOnRefreshUrl { get; set; }
	}
}
