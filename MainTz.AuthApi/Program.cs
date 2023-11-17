using MainTz.AuthApi.Services.Abstractions;
using MainTz.AuthApi.Services;
using Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

app.Run(async (context) =>
{
	if(context.Request.Path == "/GetAccessToken")
	{
		using StreamReader reader = new StreamReader(context.Request.Body);
		string role = await reader.ReadToEndAsync();
		var tokenService = app.Services.GetRequiredService<ITokenService>();

		Roles resultEnum;
		Enum.TryParse(role, out resultEnum);

		var token = tokenService.CreateAccessToken(resultEnum);
		await context.Response.WriteAsync(token);
	}
});

app.Run();