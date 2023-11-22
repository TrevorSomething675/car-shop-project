using MainTz.AuthApi.Services.Abstractions;
using MainTz.AuthApi.Services;
using Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

app.Run(async (context) =>
{
	if(context.Request.Path == "/GetTokens")
	{
		using StreamReader reader = new StreamReader(context.Request.Body);
		string role = await reader.ReadToEndAsync();
		var resultrole = role.Replace("=", ""); 
		var tokenService = app.Services.GetRequiredService<ITokenService>();

		Roles resultEnum;
		Enum.TryParse(resultrole, out resultEnum);

		var accessToken = tokenService.CreateAccessToken(resultEnum);
        var refreshToken = tokenService.CreateRefreshToken(resultEnum);

		await context.Response.WriteAsJsonAsync(new { accessToken, refreshToken });
	}
});

app.Run();