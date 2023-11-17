using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Extensions.SettingsModels;
using System.Text;
using MainTz.RestApi.dal.Data.Models.Entities;

namespace MainTz.RestApi.Configurations.AuthConfigration
{
    public static class AuthConfiguration
	{
		public static IServiceCollection AddAppAuth(this IServiceCollection services, AuthSettings identitySettings)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(jwt =>
				{
					jwt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = identitySettings.Issuer,
						ValidAudience = identitySettings.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings.SecretKey))
					};
				});

			services.AddAuthorization(options =>
			{
				options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser().Build();
			});

			services.AddIdentity<User, IdentityRole<int>>()
				.AddEntityFrameworkStores<MainContext>()
				.AddUserManager<UserManager<User>>()
				.AddSignInManager<SignInManager<User>>();

			return services;
		}

		public static void UseAppAuth(this WebApplication app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
		}
	}
}
