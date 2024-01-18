using FluentValidation.AspNetCore;
using System.Reflection;

namespace MainTz.Web.Configurations
{
    public static class ValidatorsConfiguration
    {
        public static IServiceCollection AddAppValidators(this IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

            return services;
        }
    }
}