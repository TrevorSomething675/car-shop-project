namespace MainTz.RestApi.BLL.Middlewares
{
    /// <summary>
    /// Этот middleware добавляет токен в заголовк при каждом запросе
    /// </summary>
    public class JwtHeaderMiddleware
    {
        private readonly RequestDelegate next;


    }
}
