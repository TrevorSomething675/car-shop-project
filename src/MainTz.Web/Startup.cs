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
                                Name = "Баварские Моторные Заводы",
                                Description = "В Лейпциге (Германия) расположено головное предприятие производителя БМВ.",
                                City = "Лейпциг"
							},
                            new ManufacturerEntity
                            {
                                Name = "Дженерал Моторс Континенталь",
                                Description = "“General Motors Continental” была организована на территории старого монастыря в Антверпене.",
                                City = "Детройт"
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
                                Name = "BMW"
                            },
                            new BrandEntity
                            {
                                Name = "Chevrolet"
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
                                Name = "BMW 5 серии 2012",
                                IsVisible = true,
                                Price = 1899000,
                                Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
                                Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
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
                                },
                                Images = new List<ImageEntity> {
                                    new ImageEntity
                                    {
                                        Name = "BMW 5 серии 2012-1",
                                        Path = "cars-image-bucket/BMW 5 серии 2012-1.jpg"
                                    },
                                    new ImageEntity
                                    {
                                        Name = "BMW 5 серии 2012-2",
                                        Path = "cars-image-bucket/BMW 5 серии 2012-2.jpg"
                                    },
                                    new ImageEntity
                                    {
                                        Name = "BMW 5 серии 2012-3",
                                        Path = "cars-image-bucket/BMW 5 серии 2012-3.jpg"
                                    }
                                }
                            },
                            new CarEntity
                            {
                                Name = "Chevrolet Cruze 2011",
                                IsVisible = true,
                                Price = 899000,
                                Brand = context.Brands.FirstOrDefault(b => b.Name == "Chevrolet"),
                                Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Дженерал Моторс Континенталь"),
                                Description = new DescriptionEntity
                                {
                                    Color = "Синий",
                                    MaxSpeed = "170 км/ч",
                                    TypeOfDrive = "Передний привод",
                                    EnginePower = "124 л.с.",
                                    Guarantee = "2 года",
                                    KPP = "Автомат",
                                    OilType = "Бензин",
                                    Count = 2,
                                    ShortDescription = "Chevrolet Cruze 2011 - это компактный седан с превосходным дизайном и отличными характеристиками. " +
                                    "Автомобиль оснащен мощным и экономичным двигателем, который обеспечивает плавное и динамичное движение. Внутренний" +
                                    " интерьер автомобиля оформлен современными материалами высокого качества, обеспечивая комфорт и удобство во время поездок. " +
                                    "Chevrolet Cruze 2011 также оснащен передовыми технологиями безопасности и развлечений, делая его идеальным выбором " +
                                    "для городских и загородных поездок."
                                },
                                Images = new List<ImageEntity>
                                {
                                    new ImageEntity
                                    {
                                        Name = "Chevrolet Cruze 2011-1",
										Path = "cars-image-bucket/Chevrolet Cruze 2011-1.jpg"
									},
									new ImageEntity
									{
										Name = "Chevrolet Cruze 2011-2",
										Path = "cars-image-bucket/Chevrolet Cruze 2011-2.jpg"
									},
									new ImageEntity
									{
										Name = "Chevrolet Cruze 2011-3",
										Path = "cars-image-bucket/Chevrolet Cruze 2011-3.jpg"
									}
								}
                            },
							new CarEntity
							{
								Name = "BMW X3 2018",
								IsVisible = true,
								Price = 4399000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Белый",
									MaxSpeed = "175 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "190 л.с.",
									Guarantee = "2 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 1,
									ShortDescription = "BMW X3 2018 - это кроссовер класса люкс, который сочетает в себе высокую производительность, комфорт " +
                                    "и стильный дизайн. Автомобиль оснащен мощным двигателем, который обеспечивает плавное ускорение и отличную управляемость" +
                                    " на дороге. Внутреннее пространство выполнено из высококачественных материалов, создавая атмосферу роскоши и комфорта" +
                                    " для пассажиров. BMW X3 2018 также обладает передовыми технологиями, включая инфотейнмент систему, навигацию и системы" +
                                    " безопасности, что делает его идеальным выбором для тех, кто ценит высокий уровень комфорта и безопасности при вождении."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW X3 2018-1",
										Path = "cars-image-bucket/BMW X3 2018-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X3 2018-2",
										Path = "cars-image-bucket/BMW X3 2018-2.jpg"
									},
                                    new ImageEntity
									{
										Name = "BMW X3 2018-3",
										Path = "cars-image-bucket/BMW X3 2018-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW 2 серии 2020",
								IsVisible = true,
								Price = 3250000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Белый",
									MaxSpeed = "220 км/ч",
									TypeOfDrive = "Передний привод",
									EnginePower = "140 л.с.",
									Guarantee = "2 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 1,
									ShortDescription = "BMW 2 серии 2020 - это стильный и спортивный автомобиль, созданный для настоящих ценителей динамичного вождения. " +
                                    "Он сочетает в себе элегантный дизайн и высокую производительность. В этой модели использованы самые современные технологии и " +
                                    "инновационные функции, которые делают ее одним из лидеров в своем классе. В салоне BMW 2 серии 2020 вы найдете премиальные " +
                                    "материалы, удобные сиденья и передовую информационно-развлекательную систему. Этот автомобиль предлагает отличную управляемость, " +
                                    "динамичное ускорение и надежность на любой дороге."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW 2 серии 2020-1",
										Path = "cars-image-bucket/BMW 2 серии 2020-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW 2 серии 2020-2",
										Path = "cars-image-bucket/BMW 2 серии 2020-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW 2 серии 2020-3",
										Path = "cars-image-bucket/BMW 2 серии 2020-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW X1 2020",
								IsVisible = true,
								Price = 4199000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Синий",
									MaxSpeed = "210 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "150 л.с.",
									Guarantee = "3 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 1,
									ShortDescription = "BMW X1 2020 - это стильный и производительный кроссовер компактного класса, который" +
                                    " сочетает в себе спортивный дизайн, комфорт и технологии наивысшего уровня. Автомобиль оснащен мощным" +
                                    " и экономичным двигателем, обеспечивающим отличную динамику и управляемость на дороге. Внутреннее " +
                                    "пространство удобно и функционально организовано, современные системы безопасности и развлечений " +
                                    "обеспечивают высокий уровень комфорта и удовольствия от вождения. BMW X1 2020 - это идеальный выбор " +
                                    "для тех, кто ценит надежность, стиль и инновации в автомобильном мире."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW X1 2020-1",
										Path = "cars-image-bucket/BMW X1 2020-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X1 2020-2",
										Path = "cars-image-bucket/BMW X1 2020-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X1 2020-3",
										Path = "cars-image-bucket/BMW X1 2020-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW 3 серии 2013",
								IsVisible = true,
								Price = 4199000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Чёрный",
									MaxSpeed = "210 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "150 л.с.",
									Guarantee = "3 года",
									KPP = "Автомат",
									OilType = "Дизель",
									Count = 1,
									ShortDescription = "BMW 3 Серия 2013 года - это стильное и спортивное седан, сочетающее в себе роскошный дизайн," +
                                    " высокие технологии и динамичные характеристики. Автомобиль оснащен мощными бензиновыми и дизельными двигателями," +
                                    " которые обеспечивают отличную производительность и динамику. Внутри салон выполнен из высококачественных" +
                                    " материалов, и предлагает просторное пространство для пассажиров и багажа. Также в автомобиле присутствует" +
                                    " множество передовых технологий, таких как система навигации, адаптивный круиз-контроль, система помощи при" +
                                    " парковке и многое другое. BMW 3 Серия 2013 года является идеальным выбором для тех, кто ценит комфорт, стиль " +
                                    "и высокую производительность."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW 3 серии 2013-1",
										Path = "cars-image-bucket/BMW 3 серии 2013-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW 3 серии 2013-2",
										Path = "cars-image-bucket/BMW 3 серии 2013-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW 3 серии 2013-3",
										Path = "cars-image-bucket/BMW 3 серии 2013-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW X6 2021",
								IsVisible = true,
								Price = 11479000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Чёрный",
									MaxSpeed = "240 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "340 л.с.",
									Guarantee = "2 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 7,
									ShortDescription = "BMW X6 2021 - это элегантный и мощный кроссовер, который привлекает внимание своим " +
                                    "динамичным дизайном и выдающейся производительностью. Снаружи автомобиль имеет агрессивный и спортивный " +
                                    "стиль, с выразительными линиями и характерной решеткой радиатора.Внутри X6 2021 просторный и уютный салон, " +
                                    "оформленный высококачественными материалами, современной технологией и эргономичным дизайном. " +
                                    "Водитель и пассажиры могут наслаждаться комфортабельными сиденьями, удобным мультимедийным " +
                                    "интерфейсом и полным набором современных опций.Под капотом BMW X6 2021 можно найти мощные" +
                                    " бензиновые и дизельные двигатели, которые обеспечивают высокую производительность и эффективность." +
                                    " Автомобиль оснащен передовыми системами безопасности и управления, что делает его одним из лучших выборов в своем классе."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW X6 2021-1",
										Path = "cars-image-bucket/BMW X6 2021-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X6 2021-2",
										Path = "cars-image-bucket/BMW X6 2021-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X6 2021-3",
										Path = "cars-image-bucket/BMW X6 2021-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW X1 2011",
								IsVisible = true,
								Price = 1289000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Коричневый",
									MaxSpeed = "190 км/ч",
									TypeOfDrive = "Задний привод",
									EnginePower = "150 л.с.",
									Guarantee = "1 год",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 7,
									ShortDescription = "BMW X1 2011 - премиальный компактный кроссовер от немецкого производителя BMW. Автомобиль " +
                                    "отличается стильным и динамичным дизайном, характерным для всех моделей бренда BMW. Внутри X1 2011 предлагает" +
                                    " просторный и комфортабельный салон с качественными материалами отделки. Автомобиль оснащен мощными и " +
                                    "эффективными двигателями, которые обеспечивают отличную динамику и экономичность. BMW X1 2011 также обладает" +
                                    " отличной управляемостью и уверенным поведением на дороге благодаря передовой технологии подвески и системы " +
                                    "управления. Этот автомобиль идеально подходит для городской езды и дальних поездок, сочетая в себе стиль, комфорт и производительность."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW X1 2011-1",
										Path = "cars-image-bucket/BMW X1 2011-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X1 2011-2",
										Path = "cars-image-bucket/BMW X1 2011-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X1 2011-3",
										Path = "cars-image-bucket/BMW X1 2011-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW 4 серии 2014",
								IsVisible = true,
								Price = 2899000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Белый",
									MaxSpeed = "190 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "184 л.с.",
									Guarantee = "2 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 7,
									ShortDescription = "BMW 4 серии 2014 года - это стильный и спортивный автомобиль с элегантным дизайном кузова" +
									" и высоким уровнем производительности. Он оснащен мощным двигателем, который обеспечивает высокую динамику " +
									"и отличную управляемость. В салоне автомобиля присутствует высококачественная отделка и передовые технологии," +
									" которые обеспечивают комфорт и безопасность во время поездок. BMW 4 серии 2014 - это идеальный выбор для тех," +
									" кто ценит стиль, качество и динамичный образ жизни."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW 4 серии 2014-1",
										Path = "cars-image-bucket/BMW 4 серии 2014-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW 4 серии 2014-2",
										Path = "cars-image-bucket/BMW 4 серии 2014-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW 4 серии 2014-3",
										Path = "cars-image-bucket/BMW 4 серии 2014-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW 2 серии 2020",
								IsVisible = true,
								Price = 2899000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Красный",
									MaxSpeed = "200 км/ч",
									TypeOfDrive = "Передний привод",
									EnginePower = "140 л.с.",
									Guarantee = "2 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 7,
									ShortDescription = "BMW 2 серии 2020 - это стильный и спортивный компактный автомобиль, представляющий " +
									"собой идеальное сочетание элегантности и производительности. Он оснащен мощным двигателем, обеспечивающим" +
									" динамичное ускорение и отличную динамику езды. Внешне автомобиль выглядит современно и привлекательно, " +
									"с характерным дизайном BMW, выразительными линиями и гармоничными пропорциями. Внутри салон оформлен с" +
									" использованием высококачественных материалов, предлагая комфортное и уютное пространство для водителя и" +
									" пассажиров. Технологии и функции в салоне обеспечивают комфорт и удобство во время поездок. В общем, BMW " +
									"2 серии 2020 - это идеальный выбор для тех, кто ценит сочетание стиля, производительности и комфорта."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW 2 серии 2020-Red-1",
										Path = "cars-image-bucket/BMW 2 серии 2020-Red-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW 2 серии 2020-Red-2",
										Path = "cars-image-bucket/BMW 2 серии 2020-Red-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW 2 серии 2020-Red-3",
										Path = "cars-image-bucket/BMW 2 серии 2020-Red-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW X5 2017",
								IsVisible = true,
								Price = 3688000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Чёрный",
									MaxSpeed = "170 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "218 л.с.",
									Guarantee = "2 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 7,
									ShortDescription = "BMW X5 2017 - это внушительный и роскошный SUV, который сочетает в себе высокую производительность и стильный дизайн. " +
									"Этот автомобиль оснащен мощным и эффективным двигателем, который обеспечивает плавное и быстрое ускорение. Салон X5 2017 выполнен из " +
									"высококачественных материалов, с элегантным дизайном и множеством современных технологий, таких как навигационная система, система " +
									"управления мультимедиа и светодиодные фары. Кроме того, этот автомобиль обладает прекрасной управляемостью и комфортной поездкой как" +
									" по городским улицам, так и на открытых дорогах. BMW X5 2017 - идеальный выбор для тех, кто ценит роскошь и динамичность в одном автомобиле."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW X5 2017-1",
										Path = "cars-image-bucket/BMW X5 2017-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X5 2017-2",
										Path = "cars-image-bucket/BMW X5 2017-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X5 2017-3",
										Path = "cars-image-bucket/BMW X5 2017-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW X6 2015",
								IsVisible = true,
								Price = 3980000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Белый",
									MaxSpeed = "190 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "249 л.с.",
									Guarantee = "4 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 2,
									ShortDescription = "BMW X6 2015 - это роскошный и стильный кроссовер, который сочетает в себе " +
									"элегантный дизайн, высокую производительность и передовые технологии. Этот автомобиль оборудован " +
									"мощным двигателем, обеспечивающим плавное и динамичное ускорение, а также комфортным салоном с " +
									"премиальными материалами и передовыми системами безопасности. BMW X6 2015 идеально подойдет как " +
									"для ежедневных поездок по городу, так и для путешествий на дальние расстояния, предоставляя своему" +
									" владельцу высочайший уровень комфорта и удовольствия от вождения."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW X6 2015-1",
										Path = "cars-image-bucket/BMW X6 2015-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X6 2015-2",
										Path = "cars-image-bucket/BMW X6 2015-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X6 2015-3",
										Path = "cars-image-bucket/BMW X6 2015-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW X6 M 2023",
								IsVisible = true,
								Price = 23689000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Чёрный",
									MaxSpeed = "190 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "625 л.с.",
									Guarantee = "4 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 4,
									ShortDescription = "BMW X6 M 2023 - это мощный и элегантный спортивный кроссовер, " +
									"который предлагает уникальное сочетание стиля, комфорта и производительности. " +
									"Автомобиль оснащен высокотехнологичным двигателем мощностью более 600 лошадиных " +
									"сил, что позволяет ему разгоняться до 100 км/ч всего за несколько секунд. Особенности " +
									"данной модели включают в себя спортивный дизайн с агрессивными линиями, большие диски с " +
									"покрышками высокого класса, а также улучшенную аэродинамику для максимальной производительности. " +
									"Внутреннее пространство автомобиля оборудовано самыми современными технологиями, включая " +
									"цифровую приборную панель, мультимедийную систему и систему навигации. BMW X6 M 2023 " +
									"идеально подойдет для тех, кто ценит роскошь, комфорт и скорость. Этот автомобиль будет " +
									"отличным выбором для тех, кто желает иметь идеальное сочетание производительности и стиля."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW X6 M 2023-1",
										Path = "cars-image-bucket/BMW X6 M 2023-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X6 M 2023-2",
										Path = "cars-image-bucket/BMW X6 M 2023-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X6 M 2023-3",
										Path = "cars-image-bucket/BMW X6 M 2023-3.jpg"
									}
								}
							},
							new CarEntity
							{
								Name = "BMW X5 M 2021",
								IsVisible = true,
								Price = 14999000,
								Brand = context.Brands.FirstOrDefault(b => b.Name == "BMW"),
								Manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == "Баварские Моторные Заводы"),
								Description = new DescriptionEntity
								{
									Color = "Синий",
									MaxSpeed = "190 км/ч",
									TypeOfDrive = "Полный привод",
									EnginePower = "625 л.с.",
									Guarantee = "2 года",
									KPP = "Автомат",
									OilType = "Бензин",
									Count = 4,
									ShortDescription = "BMW X5 M 2021 - это спортивный внедорожник высшего класса, сочетающий в себе мощность, роскошь и" +
									" технологии. Он оснащен мощным 4,4-литровым твин-турбо V8 двигателем, который развивает более 600 лошадиных сил. " +
									"Современные технологии и системы безопасности делают вождение комфортным и безопасным. Внутри автомобиля просторный " +
									"и роскошный салон, оборудованный самыми современными возможностями развлечений и комфорта. BMW X5 M 2021 - идеальный " +
									"выбор для тех, кто ценит роскошное и высокотехнологичное вождение."
								},
								Images = new List<ImageEntity>
								{
									new ImageEntity
									{
										Name = "BMW X5 M 2021-1",
										Path = "cars-image-bucket/BMW X5 M 2021-1.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X5 M 2021-2",
										Path = "cars-image-bucket/BMW X5 M 2021-2.jpg"
									},
									new ImageEntity
									{
										Name = "BMW X5 M 2021-3",
										Path = "cars-image-bucket/BMW X5 M 2021-3.jpg"
									}
								}
							},
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