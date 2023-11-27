﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Extensions.SettingsModels;
using System.Text;

namespace MainTz.RestApi.Configurations.AuthConfigration
{
	/// <summary>
	/// Это класс, который отвечает за конфигурацию аутентификации, AddAppAuth метод расширения для IServiceCollection
	/// UseAppAuth метод расширения для WebApplication
	/// </summary>
	public static class AuthConfiguration
	{
		public static IServiceCollection AddAppAuth(this IServiceCollection services, AuthSettings authSettings)
		{
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
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

            services.AddAuthorization();

            return services;
		}

		public static void UseAppAuth(this WebApplication app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
		}
	}
}
