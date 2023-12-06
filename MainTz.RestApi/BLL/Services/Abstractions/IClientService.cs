using MainTz.RestApi.DAL.Data.Models.Models;

namespace MainTz.RestApi.BLL.Services.Abstractions
{
	public interface IClientService
	{
		public Task<TokensModel> SendRequestAsync(string url, string message);
		public Task<TokensModel> SendRequestWithTokenAsync(string url, string message, string token);
    }
}
