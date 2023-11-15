using MainTz.RestApi.Repositories.Abstractions;
using MainTz.RestApi.Services.Abstractions;
using MainTz.RestApi.Repositories;
using MainTz.RestApi.Services;

namespace MainTz.RestApi
{
    public static class bootstrapper
    {
        public static IServiceCollection AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICarRepository, CarRepository>();

            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<ICarService, CarService>();

            return services;
        }
    }
}
