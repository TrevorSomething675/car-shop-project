namespace MainTz.RestApi.BLL.Services.Abstractions
{
	public interface IClientService
	{
		public Task<string> SendRequest(string url, string message);
	}
}
