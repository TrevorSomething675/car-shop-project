using MainTz.AuthApi.Services.Abstractions;
using MainTz.AuthApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

app.Run(async (context) =>
{
	if(context.Request.Path == "/GetAccessToken")
	{
		var tokenService = app.Services.GetRequiredService<ITokenService>();
		var token = tokenService.CreateAccessToken(Extensions.Roles.User);
		await context.Response.WriteAsync(token);
	}
});

app.Run();
