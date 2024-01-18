using MainTz.Application.Models.SittingsModels;
using MainTz.Core.Options;

namespace MainTz.Web.Configurations
{
    public static class OptionsConfiguration
    {
        public static void AddAppOptionsConfiguration(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.Configure<JwtAuthSettings>(configuration.GetSection(JwtAuthSettings.JwtAuthPosition));
            services.Configure<DataBaseSettings>(configuration.GetSection(DataBaseSettings.DataBasePosition));
            services.Configure<MinioSettings>(configuration.GetSection(MinioSettings.MinioPosition));
        }
    }
}