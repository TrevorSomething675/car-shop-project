using Microsoft.AspNetCore.Authentication.JwtBearer;
using MainTz.AuthApi.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Extensions.Models.AuthModels;
using Extensions.SettingsModels;
using MainTz.AuthApi.Services;
using MainTz.AuthApi.Models;
using System.Text.Json;
using System.Text;
using Extensions;

var builder = WebApplication.CreateBuilder(args);

var jwtAuthSettings = Settings.Load<AuthSettings>("JwtAuthSettings");

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = jwtAuthSettings.Issuer,
			ValidateAudience = true,
			ValidAudience = jwtAuthSettings.Audience,
			ValidateLifetime = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthSettings.Key)),
			ValidateIssuerSigningKey = true,
		};
	});
var app = builder.Build();

app.Map("/GetTokens", async (context) =>
{
    using StreamReader reader = new StreamReader(context.Request.Body);
    string bodyContent = await reader.ReadToEndAsync();
    var content = JsonSerializer.Deserialize<RefreshTokenModel>(bodyContent);

    var tokenService = app.Services.GetRequiredService<ITokenService>();
    var userAuthModel = new UserAuthModel
    {
        Role = content.Role,
        AccessToken = tokenService.CreateAccessToken(content.Role),
        RefreshToken = tokenService.CreateRefreshToken(content.Role),
    };
    await context.Response.WriteAsJsonAsync(userAuthModel);
});

app.Map("/GetTokensOnRefresh", async (context) =>
{
    using StreamReader reader = new StreamReader(context.Request.Body);
    string bodyContent = await reader.ReadToEndAsync();
    var content = JsonSerializer.Deserialize<RefreshTokenModel>(bodyContent);

    if (!await CheckJwtTokenAsync(content.RefreshToken))
    {
        var tokenService = app.Services.GetRequiredService<ITokenService>();
        var userAuthModel = new UserAuthModel
        {
            Role = content.Role,
            AccessToken = tokenService.CreateAccessToken(content.Role),
            RefreshToken = tokenService.CreateRefreshToken(content.Role),
        };
        await context.Response.WriteAsJsonAsync(userAuthModel);
    }
    else
    {
        await context.Response.WriteAsync("Просрочен токен");
    }
});

app.Map("/", async (context) =>
{
	var response = context.Response;
	response.ContentType = "text/html; charset=utf-8";
    await response.WriteAsync("<h2>Auth Service</h2>");
});

async Task<bool> CheckJwtTokenAsync(string token)
{
    if (token == null)
        return false;

    var handler = new JwtSecurityTokenHandler();
    var jwtTokenAccessToken = handler.ReadJwtToken(token);
    var accessTokenValidTo = jwtTokenAccessToken.ValidTo;

    if(accessTokenValidTo > DateTime.Now)
        return false;

    return true;
}

app.Run();