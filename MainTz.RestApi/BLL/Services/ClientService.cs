using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.DAL.Data.Models.Models;

namespace MainTz.RestApi.BLL.Services
{
	public class ClientService : IClientService
	{
		public async Task<TokensModel> SendRequest(string url, string message)
		{
			TokensModel tokens;
            try
			{
				var client = new HttpClient();
				StringContent requestMessage = new StringContent(message);
				using var request = new HttpRequestMessage(HttpMethod.Post, url);
				request.Content = requestMessage;
				using var response = await client.SendAsync(request);
                tokens = await response.Content.ReadFromJsonAsync<TokensModel>();
			}
			catch (Exception ex)
			{
				return null; // Уточнить
			}

			return tokens;
		}
	}
}