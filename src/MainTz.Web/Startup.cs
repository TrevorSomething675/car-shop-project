using Microsoft.EntityFrameworkCore;
using MainTz.Web.Configurations;
using MainTz.Database.Entities;
using MainTa.Database.Context;
using MainTz.Web.Middleware;
using MainTz.Web.Mappings;
using System.Reflection;
using FluentValidation;

namespace MainTz.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppOptionsConfiguration();
            services.AddDbContextFactory<MainContext>();
            services.AddAppAuth();
            services.AddAppMinioConfiguration();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddDistributedMemoryCache();
            services.AddAppServices();
            services.AddAppRepositories();
            services.AddAppAutoMapper();
            
			services.AddHttpContextAccessor();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHsts();
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
                {
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                    context.SaveChanges();
                    if (!context.Manufacturers.Any())
                    {
                        var manufacturers = new List<ManufacturerEntity> {
                            new ManufacturerEntity
                            {
                                Name = "Manufacturer1",
                                Description = "Description",
                                City = "Moskow"
                            }
                        };
                        context.Manufacturers.AddRange(manufacturers);
                        context.SaveChanges();
                    }
                    if (!context.Brands.Any())
                    {
                        var brands = new List<BrandEntity>
                        {
                            new BrandEntity
                            {
                                Name = "brand1"
                            },
                            new BrandEntity
                            {
                                Name = "brand2"
                            },
                        };
                        context.Brands.AddRange(brands);
                        context.SaveChanges();
                    }
                    if (!context.Roles.Any())
                    {
                        var roles = new List<RoleEntity>
                        {
                            new RoleEntity { Name = "Admin" },
                            new RoleEntity { Name = "Manager" },
                            new RoleEntity { Name = "User" }
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
                                Name = "Manager1",
                                Email = "Manager1@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.Name == "Manager").FirstOrDefault()
                            },
                            new UserEntity
                            {
                                Name = "Admin",
                                Email = "Admin@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.Name == "Admin").FirstOrDefault(),
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
                                        IsRead = false,
                                        SendedDate = DateTime.UtcNow
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
                                        IsRead = false,
										SendedDate = DateTime.UtcNow
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
                                        IsRead = false,
										SendedDate = DateTime.UtcNow
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
                                        IsRead = true,
										SendedDate = DateTime.UtcNow
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
                                        IsRead = true,
										SendedDate = DateTime.UtcNow
									},
                                }
                            },
                            new UserEntity
                            {
                                Name = "User",
                                Email = "User@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.Name == "User").FirstOrDefault(),
                                Notifications = new List<NotificationEntity>
                                {
                                    new NotificationEntity
                                    {
                                        Header = "testHeader1",
                                        Description = "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1" +
                                        "Description1Description1Description1Description1Description1",
										SendedDate = DateTime.UtcNow
									},
                                    new NotificationEntity
                                    {
                                        Header = "testHeader2",
                                        Description = "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2" +
                                        "Description2Description2Description2Description2Description2",
										SendedDate = DateTime.UtcNow
									},
                                }
                            },
                            new UserEntity
                            {
                                Name = "Manager2",
                                Email = "Manager2@mail.ru",
                                Password = "123123123Qq",
                                Role = context.Roles.Where(role => role.Name == "Manager").FirstOrDefault()
                            },
                        };
                        context.Users.AddRange(users);
                        context.SaveChanges();
                    }
                    if (!context.Cars.Any())
                    {
                        var cars = new List<CarEntity>
                        {
                            new CarEntity
                            {
                                Name = " BMW 5 серии 2012",
                                IsVisible = true,
                                Price = 1899000,
                                Brand = context.Brands.FirstOrDefault(),
                                Manufacturer = context.Manufacturers.FirstOrDefault(),
                                Description = new DescriptionEntity
                                {
                                    Color = "Чёрный",
                                    MaxSpeed = "190 км/ч",
                                    TypeOfDrive = "Задний привод",
                                    EnginePower = "245 л.с.",
                                    Guarantee = "1 год",
                                    KPP = "Автомат",
                                    OilType = "Бензин",
                                    Count = 6,
                                    ShortDescription = "BMW 5 серии 2012 года - это роскошный и элегантный седан премиум-класса, сочетающий " +
                                    "в себе высокий уровень комфорта, динамичную производительность и передовые технологии. Автомобиль отличается изысканным " +
                                    "дизайном и вниманием к деталям, что делает его одним из популярных выборов среди ценителей авто во всем мире."
                                }
                            }
                        };

                        cars[0].Images = new List<ImageEntity> { 
                            new ImageEntity 
                            {
                                Name = "pic1",
                                Path = "cars-image-bucket/BMW 5 серии 2012-1.jpg"
                            },
                            new ImageEntity
                            {
                                Name = "pic2",
                                Path = "cars-image-bucket/BMW 5 серии 2012-2.jpg"
                            },
                            new ImageEntity
                            {
                                Name = "pic2",
                                Path = "cars-image-bucket/BMW 5 серии 2012-3.jpg"
                            }
                        };
                        context.Cars.AddRange(cars);
                        context.SaveChanges();
                        /*
                        cars[1].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/BMW-X6-2016.jpg",
                            }
                        };
                        cars[2].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/Audi-RS-Q8-2021.jpg",
                            }
                        };
                        cars[3].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/BMW-X7-2019.jpg",
                            }
                        };
                        cars[4].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/LiXiang-L7.jpg",
                            }
                        };
                        cars[5].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/LiXiang-L9.jpg",
                            }
                        };
                        cars[6].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/Audi-Q7.jpg",
                            }
                        };
                        cars[7].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/BMW-520d-Xdrive.jpg",
                            }
                        };
                        cars[8].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/BMW-520d-Xdrive.jpg",
                            }
                        };
                        cars[9].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/BMW-M4-Competition.jpg",
                            }
                        };
                        #endregion
                        var adminUser = context.Users.
                            Include(u=>u.Cars)
                            .First(user => user.Role.Name == "Admin");
                        var favorCar = context.Cars.First();
                        adminUser.Cars.Add(favorCar);
                        context.SaveChanges();
                        */
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
            app.UseAppAuth();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
	            name: "default",
	            pattern: "{controller=Car}/{action=GetCars}");
			});
        }
    }
}