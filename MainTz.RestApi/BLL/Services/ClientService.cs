using MainTz.RestApi.BLL.Services.Abstractions;

namespace MainTz.RestApi.BLL.Services
{
	public class ClientService : IClientService
	{
		public async Task<string> SendRequest(string url)
		{
			string result;
			try
			{
				using (var client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync(url);
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
