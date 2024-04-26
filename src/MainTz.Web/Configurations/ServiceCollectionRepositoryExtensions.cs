using MainTz.Infrastructure.Repositories;
using MainTz.Application.Repositories;

namespace MainTz.Web.Configurations
{
    public static class ServiceCollectionRepositoryExtensions
    {
        public static IServiceCollection AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICarRepository, CarRepository>();

            return services;
        }
    }
}
