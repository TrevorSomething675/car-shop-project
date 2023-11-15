using AutoMapper;
using MainTz.RestApi.Configurations.AutoMapperConfiguration.Mappings;

namespace MainTz.RestApi.Configurations.AutoMapperConfiguration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Mapper { get; set; } = null!;

        public static IServiceCollection AddAppAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<CarMap>();
            });

            return services;
        }
    }
}
