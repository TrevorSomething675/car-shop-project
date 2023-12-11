using MainTz.Infrastructure.Mappings.DomainDbEntityMappings.User;
using MainTz.Infrastructure.Mappings.RequestDomainMappings.User;
using MainTz.Infrastructure.Mappings.DomainDbEntityMappings.Car;
using MainTz.Infrastructure.Mappings.RequestDomainMappings.Car;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings
{
    /// <summary>
    /// Включение маппингов в работу
    /// </summary>
    public static class AutoMapperConfiguration
    {
        public static IMapper Mapper { get; set; } = null!;

        public static IServiceCollection AddAppAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<RequestDomainCarMap>();
                config.AddProfile<ResponseDomainCarMap>();
                config.AddProfile<DomainDbEntityCarMap>();

                config.AddProfile<RequestDomainUserMap>();
                config.AddProfile<ResponseDomainUserMap>();
                config.AddProfile<DomainDbEntityUserMap>();
            });

            return services;
        }
    }
}