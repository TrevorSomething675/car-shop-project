using MainTz.Infrastructure.Services;
using MainTz.Application.Services;

namespace MainTz.Web.Configurations
{
    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFavoriteCarService, FavoriteCarService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IMinioService, MinioService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();

            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
