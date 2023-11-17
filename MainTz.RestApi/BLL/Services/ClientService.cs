using MainTz.RestApi.BLL.Services.Abstractions;

namespace MainTz.RestApi.BLL.Services
{
	public class ClientService : IClientService
	{
		public async Task<string> SendRequest(string url, string message)
		{
			string result;
			try
			{
				using (var client = new HttpClient())
				{
					StringContent requestMessage = new StringContent(message);
					using var request = new HttpRequestMessage(HttpMethod.Post, url);
					request.Content = requestMessage;
					using var response = await client.SendAsync(request);

					result = await response.Content.ReadAsStringAsync();
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}

			return result;
		}
	}
}