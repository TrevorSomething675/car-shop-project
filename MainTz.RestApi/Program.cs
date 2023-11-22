using MainTz.RestApi.Configurations.AutoMapperConfiguration;
using MainTz.RestApi.Configurations.NLogConfiguration;
using MainTz.RestApi.Configurations.AuthConfigration;
using MainTz.RestApi.dal.Data.Models.Entities;
using MainTz.RestApi.BLL.Middlewares;
using Microsoft.EntityFrameworkCore;
using MainTz.RestApi.Configurations;
using Extensions.SettingsModels;
using MainTz.RestApi;
using Extensions;

var builder = WebApplication.CreateBuilder(args);

var dbSettings = Settings.Load<DataBaseSettings>("DataBaseSettings");
var jwtAuthSettings = Settings.Load<AuthSettings>("JwtAuthSettings");

var services = builder.Services;

services.AddAppLogger(); // добавление логгера
services.AddAppAutoMapperConfiguration(); // конфигурация автомаппера
services.AddAppDbContext(dbSettings);
services.AddAppSwagger();

services.AddAppServices(); //Регистрация сервисов
services.AddAppRepositories(); //Регистрация репозиториев
services.AddAppAuth(jwtAuthSettings); // Аутентификация

var app = builder.Build();

#region testData
using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
    {
        try
        {
            //context.Users.AddRange(new User
            //{
            //    Name = "User",
            //    Password = "123",
            //    Role = Roles.User
            //},
            //new User
            //{
            //    Name = "Manager",
            //    Password = "123",
            //    Role = Roles.Manager
            //},
            //new User
            //{
            //    Name = "Admin",
            //    Password = "123",
            //    Role = Roles.Admin
            //});
            //context.SaveChanges();
            context.Database.Migrate();
        }
        catch
        {
            context.Database.EnsureCreated();
        }
    }
}
#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();

app.UseMiddleware<JwtHeaderMiddleware>();
app.UseAppAuth();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.UseAppSwagger();

app.Run();