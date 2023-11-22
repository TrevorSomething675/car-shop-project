using MainTz.RestApi.DAL.Data.Models.Models;

namespace MainTz.RestApi.BLL.Services.Abstractions
{
	public interface IClientService
	{
		public Task<TokensModel> SendRequest(string url, string message);
	}
}
