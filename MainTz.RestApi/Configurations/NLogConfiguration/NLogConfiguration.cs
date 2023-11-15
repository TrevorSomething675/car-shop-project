using NLog.Web;

namespace MainTz.RestApi.Configurations.NLogConfiguration
{
	public static class NLogConfiguration
	{
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        })
        .UseNLog();

        public static IServiceCollection AddAppLogger(this IServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.ClearProviders();
                config.SetMinimumLevel(LogLevel.Trace);
            });

            return services;
        }
    }
}