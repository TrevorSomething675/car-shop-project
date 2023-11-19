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
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = authSettings.Issuer,
						ValidateAudience = true,
						ValidAudience = authSettings.Audience,
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key)),
						ValidateIssuerSigningKey = true,
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
