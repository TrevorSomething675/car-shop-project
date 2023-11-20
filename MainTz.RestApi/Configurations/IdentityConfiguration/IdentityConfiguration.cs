using MainTz.RestApi.dal.Data.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MainTz.RestApi.Configurations.IdentityConfiguration
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddAppIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>().
                AddEntityFrameworkStores<MainContext>().
                AddUserManager<UserManager<User>>().
                AddSignInManager<SignInManager<User>>();

            return services;
        }
    }
}
