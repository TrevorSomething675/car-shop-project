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
                    if (!context.Brands.Any())
                    {
                        var brands = new List<BrandEntity>
                        {
                            new BrandEntity
                            {
                                Name = "brand1",
                                Models = new List<ModelEntity>
                                {
                                    new ModelEntity {Name = "Внедорожник" },
                                    new ModelEntity {Name = "Кроссовер"},
                                    new ModelEntity {Name = "Седан"},
                                    new ModelEntity {Name = "Купе"}
                                },
                            },
                            new BrandEntity
                            {
                                Name = "brand2",
                                Models = new List<ModelEntity>
                                {
                                    new ModelEntity {Name = "Model2" }
                                },
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
                                Name = "Volkswagen Touareg 2019",
                                Color = "Белый",
                                Price = 5990000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/Volkswagen-Touareg-2019.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "Volkswagen Touareg 2019 - это премиальный полноразмерный внедорожник, представляющий " +
                                "собой воплощение немецкого качества и инженерного мастерства. Этот автомобиль сочетает в себе элегантный дизайн, " +
                                "высокую проходимость и передовые технологии.\r\n\r\nЭкстерьер Touareg 2019 впечатляет своей солидностью " +
                                "и стильным внешним видом. Он имеет гладкие линии и плавные формы, которые придают ему элегантность и динамику." +
                                " Огромная радиаторная решетка с хромированными акцентами и светодиодные фары создают внушительное впечатление. " +
                                "Кузов автомобиля выполнен с использованием высококачественных материалов, что придает ему прочность и долговечность.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Внедорожник")
                            },
                            new CarEntity
                            {
                                Name = "BMW X6 2016",
                                Color = "Белый",
                                Price = 4500000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/Volkswagen-Touareg-2019.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "BMW X6 2016 - это спортивный кроссовер среднего размера, который сочетает в себе элегантный дизайн, " +
                                "высокую производительность и роскошный интерьер. Этот автомобиль предлагает уникальное сочетание стиля и функциональности, " +
                                "делая его привлекательным выбором для тех, кто ищет спортивность и комфорт в одном пакете.\r\n\r\nЭкстерьер BMW X6 2016" +
                                " выделяется своей агрессивностью и динамикой. Он имеет смелые и резкие линии, которые придают ему силу и энергию. " +
                                "Характерными особенностями являются большие колесные арки, динамическая форма крыши и характерная двухъярусная решетка радиатора." +
                                " Эти детали создают внушительный внешний вид и подчеркивают спортивный характер автомобиля.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Кроссовер")
                            },
                            new CarEntity
                            {
                                Name = "Audi RS Q8 2021",
                                Color = "Красный",
                                Price = 18000000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/Audi-RS-Q8-2021.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "Audi RS Q8 2021 - это мощный и роскошный спортивный кроссовер, который предлагает высокую производительность и" +
                                " роскошный интерьер. Этот автомобиль сочетает в себе элегантный дизайн, передовые технологии и спортивные характеристики, делая" +
                                " его идеальным выбором для тех, кто ищет комфорт и адреналин в одном пакете.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Кроссовер")
                            },
                            new CarEntity
                            {
                                Name = "BMW X7 2019",
                                Color = "Чёрный",
                                Price = 8290000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/Audi-RS-Q8-2021.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "BMW X7 2019 - это роскошный и просторный SUV, который предлагает элегантный дизайн, высокую производительность и комфортный интерьер. " +
                                "Этот автомобиль обладает внушительными размерами и привлекательными линиями, подчеркивающими его престиж и статус. Внутри вы найдете роскошный салон с" +
                                " высококачественными материалами и передовыми технологиями, создающими уютную и современную атмосферу. " +
                                "BMW X7 2019 также предлагает мощные двигатели и передовые системы управления, обеспечивая непревзойденную производительность и динамичный опыт вождения.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Внедорожник")
                            },
                            new CarEntity
                            {
                                Name = "LiXiang L7",
                                Color = "Чёрный",
                                Price = 5990000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/LiXiang-L7.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "LiXiang L7 - это электрический седан, который предлагает стильный дизайн, передовые технологии и экологическую эффективность. " +
                                "Этот автомобиль сочетает в себе элегантные линии и современные элементы, создавая привлекательный внешний вид. Внутри вы найдете просторный " +
                                "и удобный салон с передовыми системами развлечений и коммуникаций. LiXiang L7 оснащен мощной электрической системой, которая обеспечивает " +
                                "плавное и тихое движение, а также дальнюю дистанцию пробега на одной зарядке. Этот автомобиль идеально подходит для тех, кто ценит экологическую " +
                                "ответственность и современные технологии.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Седан")
                            },
                            new CarEntity
                            {
                                Name = "LiXiang L9",
                                Color = "Чёрный",
                                Price = 7300000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/LiXiang-L9.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "LiXiang L9 - это передовой электрический суперкар, который воплощает в себе элегантность, скорость и инновационные технологии. " +
                                "Его потрясающий дизайн с гладкими линиями и аэродинамической формой приковывает взгляды прохожих. Великолепный салон LiXiang L9 предлагает" +
                                " роскошный интерьер с высококачественными материалами и передовыми системами развлечений.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Седан")
                            },
                            new CarEntity
                            {
                                Name = "Audi Q7",
                                Color = "Синий",
                                Price = 6850000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/Audi-Q7.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "Audi Q7 - это внедорожник премиум-класса, который сочетает в себе элегантность, " +
                                "комфорт и высокую производительность. С его потрясающим дизайном и внушительными размерами, " +
                                "Audi Q7 привлекает внимание на дороге.\r\n\r\nЭтот автомобиль имеет роскошный интерьер с" +
                                " высококачественными материалами и передовыми технологиями. Просторный салон Audi Q7 предлагает " +
                                "комфортные сиденья и множество возможностей для настройки, чтобы каждая поездка была максимально приятной.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Внедорожник")
                            },
                            new CarEntity
                            {
                                Name = "BMW 520d Xdrive",
                                Color = "Синий",
                                Price = 5450000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/Audi-Q7.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "BMW 520d xDrive - это роскошный седан, который объединяет в себе элегантность, " +
                                "динамичность и превосходную экономичность. С его изящным дизайном и высококачественными материалами, " +
                                "этот автомобиль выделяется на дороге. BMW 520d xDrive имеет просторный и роскошный интерьер, " +
                                "который предлагает комфорт и функциональность. Высококачественные материалы и внимательное внимание к " +
                                "деталям создают роскошную атмосферу в салоне. Комфортные сиденья и инновационные системы управления " +
                                "позволяют водителю и пассажирам наслаждаться каждой поездкой.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Седан")
                            },
                            new CarEntity
                            {
                                Name = "Skoda Kodiaq 2023",
                                Color = "Чёрный",
                                Price = 4770000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/Skoda-Kodiaq-2023.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "Skoda Kodiaq 2023 - это современный и стильный семейный " +
                                "внедорожник, который предлагает просторный интерьер, передовые технологии" +
                                " и отличную проходимость. С его элегантным дизайном и функциональностью," +
                                " Skoda Kodiaq 2023 привлекает внимание на дороге.",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Внедорожник")
                            },
                            new CarEntity
                            {
                                Name = "BMW M4 Competition",
                                Color = "Зелёный",
                                Price = 11700000,
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "pic1",
                                        Path = "cars-image-bucket/BMW-M4-Competition.jpg"
                                    }
                                },
                                IsVisible = true,
                                Description = "",
                                Model = context.Models.FirstOrDefault(m => m.Name == "Купе")
                            }
                        };

                        #region imageDataAppend
                        cars[0].Images = new List<ImageEntity> { new ImageEntity {
                            Name = "pic1",
                            Path = "cars-image-bucket/Volkswagen-Touareg-2019.jpg",
                            }
                        };
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

                        context.Cars.AddRange(cars);
                        context.SaveChanges();
                        var adminUser = context.Users.
                            Include(u=>u.Cars)
                            .First(user => user.Role.Name == "Admin");
                        var favorCar = context.Cars.First();
                        adminUser.Cars.Add(favorCar);
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