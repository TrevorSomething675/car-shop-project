using Microsoft.EntityFrameworkCore;
using Extensions.SettingsModels;
using MainTz.RestApi.dal.Data.Models.Entities;

namespace MainTz.RestApi.Configurations
{
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
