using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.DAL.Data.Models.Models;
using Newtonsoft.Json;
using System.Text;

namespace MainTz.RestApi.BLL.Services
{
	public class ClientService : IClientService
	{
        /// <summary>
        /// Отправка запроса с получением данных в модель TokensModel
        /// </summary>
        /// <param name="url"></param>
        /// <param name="message"></param>
        /// <returns></returns>

        //public async Task<TokensModel> SendRequestAsync()
        //{

        //}

        private readonly ILogger<ClientService> _logger;
        public ClientService(ILogger<ClientService> logger)
        {
            _logger = logger;
        }

        public async Task<TokensModel> SendRequestAsync(string url, string role, CancellationToken cancellationToken = default)
		{
			TokensModel tokens;
            try
            {
                var client = new HttpClient();
                var content = new { role };

                //System.Text.Json.JsonSerializer.

				var requestContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
				using var request = new HttpRequestMessage(HttpMethod.Get, url);
				request.Content = requestContent;

				using var response = await client.SendAsync(request, cancellationToken);
                return await response.Content.ReadFromJsonAsync<TokensModel>();
			}
			catch (Exception ex)
			{
                _logger.LogError($"{ex.Message}");
                return null;
			}
		}

        public async Task<TokensModel> SendRequestWithTokenAsync(string url, string role, string refreshToken)
        {
            TokensModel tokens;
            try
            {
                var client = new HttpClient();
                var content = new {role, refreshToken};

                var requestContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                using HttpRequestMessage request = new (HttpMethod.Get, url);
                request.Content = requestContent;

                using HttpResponseMessage response = await client.SendAsync(request);
                return await response.Content.ReadFromJsonAsync<TokensModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}