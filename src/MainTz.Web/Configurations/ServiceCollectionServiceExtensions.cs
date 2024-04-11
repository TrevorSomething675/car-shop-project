﻿using MainTz.Infrastructure.Services;
using MainTz.Application.Services;

namespace MainTz.Web.Configurations
{
    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFavoriteCarService, FavoriteCarService>();
            services.AddScoped<IMinioService, MinioService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICarService, CarService>();

            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
