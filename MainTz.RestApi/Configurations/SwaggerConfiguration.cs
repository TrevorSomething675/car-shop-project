namespace MainTz.RestApi.Configurations
{
    /// <summary>
    /// Конфигурация сваггера
    /// </summary>
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();

            return services;
        }

        public static void UseAppSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
