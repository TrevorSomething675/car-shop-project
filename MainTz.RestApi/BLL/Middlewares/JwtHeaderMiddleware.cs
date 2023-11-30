namespace MainTz.RestApi.BLL.Middlewares
{
    public class JwtHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }
    }
}
