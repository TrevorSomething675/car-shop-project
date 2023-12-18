using Microsoft.AspNetCore.Authentication.JwtBearer;
using MainTz.Application.Models.SittingsModels;
using MainTz.Infrastructure.Repositories;
using MainTz.Application.Repositories;
using Microsoft.IdentityModel.Tokens;
using MainTz.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MainTz.Application.Services;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using MainTz.Web.Middleware;
using MainTz.Web.ViewModels;
using MainTz.Web.Validators;
using MainTz.Web.Mappings;
using FluentValidation;
using System.Text;

namespace MainTz.Web
{
    public class Startup
    {
        DataBaseSettings _dbSettings;
        JwtAuthSettings _authSettings;
		public Startup(IConfiguration configuration)
        {
			_authSettings = configuration.GetSection(JwtAuthSettings.JwtAuthPosition).Get<JwtAuthSettings>();
			_dbSettings = configuration.GetSection(DataBaseSettings.DataBasePosition).Get<DataBaseSettings>();
		}
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options =>
            {
                options.UseNpgsql(_dbSettings.ConnectionString);
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
			services.AddTransient<ITokenService>(provider => new TokenService(_authSettings));
			services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IValidator<RegisterFormRequest>, RegisterFormValidator>();
            services.AddScoped<IValidator<RestoreEmailRequest>, RestoreEmailValidator>();
            services.AddScoped<IValidator<LoginFormRequest>, LoginFormValidator>();

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
            		ValidIssuer = _authSettings.Issuer,
            		ValidateAudience = true,
            		ValidAudience = _authSettings.Audience,
            		ValidateLifetime = true,
            		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key)),
            		ValidateIssuerSigningKey = true,
            	};
            });

			services.AddAuthorization();
		}

        public void Configure(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
                {
                    #region UserTestData
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    context.SaveChanges();
                    if (!context.Roles.Any())
                    {
                        var roles = new List<RoleEntity>
                        {
                            new RoleEntity { RoleName = "Admin" },
                            new RoleEntity { RoleName = "Manager" },
                            new RoleEntity { RoleName = "User" }
                        };
                        context.Roles.AddRange(roles);
                        context.SaveChanges();
                    }

                    if(!context.Users.Any())
                    {
                        var users = new List<UserEntity>
                        {
                            new UserEntity
                            {
                                Name = "Admin",
                                Email = "Admin@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.RoleName == "Admin").FirstOrDefault()
                            },
                            new UserEntity
                            {
                                Name = "Manager",
                                Email = "Manager@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.RoleName == "Manager").FirstOrDefault()
                            }
                        };

                        context.Users.AddRange(users);
                        context.SaveChanges();
                    }

                    #endregion

                    #region CarTestData
                    if (!context.Brands.Any())
                    {
                        var brand = new BrandEntity
                        {
                            Name = "brand1",
                            Models = new List<ModelEntity>
                            {
                                new ModelEntity {Name = "Model1" }
                            }
                        };
                        context.Brands.Add(brand);
                        context.SaveChanges();
                    }

                    if (!context.Cars.Any())
                    {
                        var cars = new List<CarEntity>();

                        for(int i = 0; i < 15; i++)
                        {
                            var car = new CarEntity
                            {
                                Name = $"TestName{i}",
                                Color = "red",
                                Images = new List<ImageEntity>
                            {
                                new ImageEntity
                                {
                                    Name = "pic1",
                                    File = new byte[5],
                                }
                            },
                                IsFavorite = true,
                                IsVisible = true,
                                Description = $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                                $"Decscriptin{i}Decscriptin{i}Decscriptin{i}",
                                Model = context.Models.FirstOrDefault()

                            };
                            cars.Add(car);
                        };
                        context.Cars.AddRange(cars);
                        context.SaveChanges();
                    }
                    #endregion
                }
            }
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
	            pattern: "{controller=User}/{action=Index}");
			});
            app.UseDeveloperExceptionPage();
        }
    }
}