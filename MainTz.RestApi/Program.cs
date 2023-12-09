using MainTz.RestApi.Configurations.AutoMapperConfiguration;
using MainTz.RestApi.Configurations.AuthConfigration;
using MainTz.RestApi.BLL.Middlewares;
using MainTz.RestApi.Configurations;
using Extensions.SettingsModels;
using MainTz.RestApi;
using Extensions;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
var dbSettings = Settings.Load<DataBaseSettings>("DataBaseSettings");
var jwtAuthSettings = Settings.Load<AuthSettings>("JwtAuthSettings");
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

var services = builder.Services;
builder.Host.UseNLog();

services.AddAppAutoMapperConfiguration();
services.AddAppDbContext(dbSettings);
services.AddAppSwagger();

services.AddAppServices();
services.AddAppRepositories();
services.AddAppAuth(jwtAuthSettings);

var app = builder.Build();

/*
#region testData
using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
    {
        try
        {
            if (context.Users.FirstOrDefault(user => user.Name == "User") == null)
            {
                context.Users.AddRange(new User
                {
                    Name = "User",
                    Password = "123",
                    Role = "User",
                    ExpTime = DateTime.UtcNow.AddMinutes(5),
                    RefreshToken = "refreshToken1"
                },
                new User
                {
                    Name = "Manager",
                    Password = "123",
                    Role = "Manager",
                    ExpTime = DateTime.UtcNow.AddMinutes(5),
                    RefreshToken = "refreshToken2"
                },
                new User
                {
                    Name = "Admin",
                    Password = "123",
                    Role = "Admin",
                    ExpTime = DateTime.UtcNow.AddMinutes(5),
                    RefreshToken = "refreshToken3"
                });
                context.SaveChanges();
            }
            context.Database.Migrate();
        }
        catch
        {
            context.Database.EnsureCreated();
        }
    }
}
//#endregion 
*/
app.UseMiddleware<JwtHeaderMiddleware>();
app.UseMiddleware<JwtRefreshMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app.UseAppServices();
app.UseAppAuth();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.UseAppSwagger();

app.Run();