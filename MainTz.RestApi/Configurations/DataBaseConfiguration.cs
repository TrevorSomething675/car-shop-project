using MainTz.RestApi.DAL.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Extensions.SettingsModels;

namespace MainTz.RestApi.Configurations
{
    /// <summary>
    /// Конфигурация db котекста
    /// </summary>
    public static class DataBaseConfiguration
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services,
            DataBaseSettings settings = null)
        {
            services.AddDbContext<MainContext>(options =>
            {
                options.UseNpgsql(settings.ConnectionString);
            });

            return services;
        }
    }
}
