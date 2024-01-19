using MainTz.Application.Models.OptionsModels;
using MainTz.Core.Options;

namespace MainTz.Web.Configurations
{
    public static class ServiceCollectionOptionsExtensions
    {
        public static void AddAppOptionsConfiguration(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.Configure<JwtAuthOptions>(configuration.GetSection(JwtAuthOptions.SectionName));
            services.Configure<DataBaseSettings>(configuration.GetSection(DataBaseSettings.SectionName));
            services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.SectionName));
        }
    }
}