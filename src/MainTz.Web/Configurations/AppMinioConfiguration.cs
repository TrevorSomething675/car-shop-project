using MainTz.Application.Models.OptionsModels;
using Microsoft.Extensions.Options;
using Minio;

namespace MainTz.Web.Configurations
{
    public static class AppMinioConfiguration
    {
        public static IServiceCollection AddAppMinioConfiguration(this IServiceCollection services)
        {

            var minioOptions = services.BuildServiceProvider().GetRequiredService<IOptions<MinioOptions>>().Value;
            services.AddMinio(configureClient => configureClient
                .WithEndpoint(minioOptions.StorageEndPoint)
                .WithCredentials(minioOptions.ROOT_USER, minioOptions.ROOT_PASSWORD)
                .WithSSL(false)
                .Build());

            return services; 
        }
    }
}
