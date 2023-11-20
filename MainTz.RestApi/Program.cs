using MainTz.RestApi.Configurations.AutoMapperConfiguration;
using MainTz.RestApi.Configurations.NLogConfiguration;
using MainTz.RestApi.Configurations.AuthConfigration;
using MainTz.RestApi.dal.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MainTz.RestApi.Configurations;
using Extensions.SettingsModels;
using MainTz.RestApi;
using Extensions;
using MainTz.RestApi.MiddleWares;
using MainTz.RestApi.Configurations.IdentityConfiguration;

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
services.AddAppIdentity(); //Настройка Identity

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
app.UseDefaultFiles();
app.UseRouting();

app.UseAppAuth();
//app.UseMiddleware<JwtMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAppSwagger();

app.Run();