using MainTz.Application.Models.OptionsModels;
using MainTz.Core.Options;

namespace MainTz.Web.Configurations
{
    public static class AppOptionsConfiguration
    {
        public static void AddAppOptionsConfiguration(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.Configure<JwtAuthOptions>(configuration.GetSection(JwtAuthOptions.JwtAuthPosition));
            services.Configure<DataBaseSettings>(configuration.GetSection(DataBaseSettings.DataBasePosition));
            services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MinioPosition));
        }
    }
}