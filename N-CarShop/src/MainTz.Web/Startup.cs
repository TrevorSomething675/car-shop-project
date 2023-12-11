using Extensions;
using MainTa.Database.Context;
using MainTz.Application.Repositories;
using MainTz.Extensions.Models;
using MainTz.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainTz.Web
{
    public class Startup
    {
		DataBaseSettings dbSettings = Settings.Load<DataBaseSettings>("DataBaseSettings");
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options =>
            {
                options.UseNpgsql(dbSettings.ConnectionString);
            });
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}