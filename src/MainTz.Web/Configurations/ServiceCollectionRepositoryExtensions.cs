using MainTz.Infrastructure.Repositories;
using MainTz.Application.Repositories;

namespace MainTz.Web.Configurations
{
    public static class ServiceCollectionRepositoryExtensions
    {
        public static IServiceCollection AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICarRepository, CarRepository>();

            return services;
        }
    }
}
