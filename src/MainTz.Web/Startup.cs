using Microsoft.AspNetCore.Authentication.JwtBearer;
using MainTz.Application.Models.SittingsModels;
using MainTz.Web.ViewModels.UserViewModels;
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
            services.AddDbContextFactory<MainContext>(options =>
            {
                options.UseNpgsql(_dbSettings.ConnectionString);
            });
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
			services.AddTransient<ITokenService>(provider => new TokenService(_authSettings));
            services.AddScoped<INotificationService, NotificationService>();
			services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IValidator<RegisterFormRequest>, RegisterFormValidator>();
            services.AddScoped<IValidator<RestoreEmailRequest>, RestoreEmailValidator>();
            services.AddScoped<IValidator<UpdateLoginUserRequest>, UpdateLoginUserValidator>();
            services.AddScoped<IValidator<UpdatePasswordUserRequest>, UpdatePasswordUserValidator>();
            services.AddScoped<IValidator<LoginFormRequest>, LoginFormValidator>();

            services.AddDomainAppAutoMapperConfiguration();
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
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
                {
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
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
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

                    if (!context.Users.Any())
                    {
                        var users = new List<UserEntity>
                        {
                            new UserEntity
                            {
                                Name = "Admin",
                                Email = "Admin@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.RoleName == "Admin").FirstOrDefault(),
                                Notifications = new List<NotificationEntity>
                                {
                                    new NotificationEntity
                                    {
                                        Header = "testHeader1",
                                        Description = "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1",
                                        IsRead = false
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader2",
                                        Description = "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2",
                                        IsRead = false
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader3",
                                        Description = "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3" +
                                        "Description3Description3Description3Description3Description3",
                                        IsRead = false
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader4",
                                        Description = "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4" +
                                        "Description4Description4Description4Description4Description4",
                                        IsRead = true
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader5",
                                        Description = "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5" +
                                        "Description5Description5Description5Description5Description5",
                                        IsRead = true
                                    },
                                }
                            },
                            new UserEntity
                            {
                                Name = "Manager",
                                Email = "Manager@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.RoleName == "Manager").FirstOrDefault()
                            },
                            new UserEntity
                            {
                                Name = "User",
                                Email = "User@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.RoleName == "User").FirstOrDefault(),
                                Notifications = new List<NotificationEntity>
                                {
                                    new NotificationEntity
                                    {
                                        Header = "testHeader1",
                                        Description = "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1"
                                    },
                                    new NotificationEntity
                                    {
                                        Header = "testHeader2",
                                        Description = "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2"
                                    },
                                }
                            },
                        };

                        context.Users.AddRange(users);
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
                                Price = i * 100,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        File = new byte[5],
                                    }
                                },
                                IsFavorite = false,
                                IsVisible = true,
                                Description = $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                                $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                                $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                                $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                                $"Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}Decscriptin{i}" +
                                $"Decscriptin{i}Decscriptin{i}Decscriptin{i}",
                                Model = context.Models.FirstOrDefault()

                            };
                            cars.Add(car);
                        };
                        context.Cars.AddRange(cars);
                        context.SaveChanges();
                    }
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
            app.UseSession();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
	            name: "default",
	            pattern: "{controller=Car}/{action=GetCars}");
			});
        }
    }
}