using MainTz.RestApi.Configurations.AutoMapperConfiguration;
using MainTz.RestApi.Configurations.NLogConfiguration;
using MainTz.RestApi.dal.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MainTz.RestApi.Configurations;
using Extensions.SettingsModels;
using MainTz.RestApi;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var dbSettings = Settings.Load<DataBaseSettings>("DataBaseSettings");

var services = builder.Services;

services.AddAppLogger();
services.AddAppAutoMapperConfiguration();
services.AddAppDbContext(dbSettings);
services.AddAppSwagger();

services.AddAppRepositories();
services.AddAppServices();

services.AddHttpContextAccessor();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.UseAppSwagger();

app.Run();