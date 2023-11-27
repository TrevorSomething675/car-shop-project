﻿using AutoMapper;
using MainTz.RestApi.Configurations.AutoMapperConfiguration.Mappings;

namespace MainTz.RestApi.Configurations.AutoMapperConfiguration
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
                config.AddProfile<CarMap>();
                config.AddProfile<UserMap>();
            });

            return services;
        }
    }
}
