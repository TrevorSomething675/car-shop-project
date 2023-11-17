using MainTz.RestApi.Configurations.AutoMapperConfiguration;
using MainTz.RestApi.Configurations.NLogConfiguration;
using MainTz.RestApi.Configurations.AuthConfigration;
using MainTz.RestApi.dal.Data.Models.Entities;
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

services.AddAppRepositories(); //Регистрация репозиториев
services.AddAppServices(); //Регистрация сервисов
services.AddAppAuth(jwtAuthSettings); // Аутентификация

services.AddAuthorization();

var app = builder.Build();

#region testData
using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
    {
        try
        {
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

app.UseRouting();

app.UseAppAuth();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAppSwagger();

app.Run();