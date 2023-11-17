using MainTz.RestApi.BLL.Services.Abstractions;
using System.Net.Http.Headers;

namespace MainTz.RestApi.BLL.Services
{
	public class ClientService : IClientService
	{
		public async Task<string> SendRequest(string url, string message)
		{
			string result;
			try
			{
				var client = new HttpClient();
				StringContent requestMessage = new StringContent(message);
				using var request = new HttpRequestMessage(HttpMethod.Post, url);
				request.Content = requestMessage;
				using var response = await client.SendAsync(request);
				result = await response.Content.ReadAsStringAsync();

				client.DefaultRequestHeaders.Authorization = 
					new AuthenticationHeaderValue("Bearer", result);
			}
			catch (Exception ex)
			{
				result = null; // Уточнить
			}

			return result;
		}
	}
}