using Microsoft.AspNetCore.Authentication.JwtBearer;
using MainTz.Infrastructure.Repositories;
using MainTz.Application.Repositories;
using Microsoft.IdentityModel.Tokens;
using MainTz.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MainTz.Application.Services;
using MainTz.Extensions.Models;
using MainTa.Database.Context;
using MainTz.Web.Middleware;
using MainTz.Web.Mappings;
using MainTz.Extensions;
using System.Text;

namespace MainTz.Web
{
    public class Startup
    {
		DataBaseSettings dbSettings = Settings.Load<DataBaseSettings>("DataBaseSettings");
        JwtAuthSettings authSettings = Settings.Load<JwtAuthSettings>("JwtAuthSettings");
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options =>
            {
                options.UseNpgsql(dbSettings.ConnectionString);
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();

            services.AddDomainAppAutoMapperConfiguration();
            services.AddControllersWithViews();
			services.AddHttpContextAccessor();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
            .AddJwtBearer(options =>
            {
            	options.RequireHttpsMetadata = true;
            	options.SaveToken = true;
            	options.TokenValidationParameters = new TokenValidationParameters
            	{
            		ValidateIssuer = true,
            		ValidIssuer = authSettings.Issuer,
            		ValidateAudience = true,
            		ValidAudience = authSettings.Audience,
            		ValidateLifetime = true,
            		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key)),
            		ValidateIssuerSigningKey = true,
            	};
            });

			services.AddAuthorization();
		}

        public void Configure(IApplicationBuilder app)
        {
			app.UseHttpsRedirection();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseMiddleware<JwtHeaderMiddleware>();
			app.UseMiddleware<JwtRefreshMiddleware>();
			app.UseMiddleware<LoggingMiddleware>();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
	            name: "default",
	            pattern: "{controller=Auth}/{action=Login}/{id?}");
			});
        }
    }
}