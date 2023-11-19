using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Extensions.SettingsModels;
using System.Text;

namespace MainTz.RestApi.Configurations.AuthConfigration
{
    public static class AuthConfiguration
	{
		public static IServiceCollection AddAppAuth(this IServiceCollection services, AuthSettings authSettings)
		{
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = false,
                            ValidateIssuerSigningKey = false,
                            ValidIssuer = authSettings.Issuer,
                            ValidAudience = authSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key))
                        };
                    });
            return services;
		}

		public static void UseAppAuth(this WebApplication app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
		}
	}
}
