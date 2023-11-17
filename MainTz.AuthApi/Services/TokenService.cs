using MainTz.AuthApi.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Extensions.SettingsModels;
using System.Security.Claims;
using System.Text;
using Extensions;

namespace MainTz.AuthApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly AuthSettings _authSettings;
        public TokenService()
        {
            _authSettings = Settings.Load<AuthSettings>("AuthSettings");
		}

        public string CreateAccessToken(Roles roles)
        {

			var claims = new List<Claim> { new Claim(ClaimTypes.Name, roles.ToString()) };
			var jwt = new JwtSecurityToken(
					issuer: _authSettings.Issuer,
					audience: _authSettings.Audience,
					claims: claims,
					expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
					signingCredentials: new SigningCredentials(
						new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(_authSettings.Key)), 
						SecurityAlgorithms.HmacSha256)
					);

			return new JwtSecurityTokenHandler().WriteToken(jwt);

            //List<Claim> claims = new List<Claim>
			//{
			//new Claim(ClaimTypes.Role, roles.ToString())
			//};

			//var creds = new SigningCredentials(
			//    new SymmetricSecurityKey(
			//        Encoding.UTF8.GetBytes(_authSettings.SecretKey)
			//        ),
			//        SecurityAlgorithms.HmacSha256
			//    );

			//var token = new JwtSecurityToken(
			//    _authSettings.Issuer,
			//    _authSettings.Audience,
			//    claims,
			//    expires: DateTime.UtcNow.AddHours(2),
			//    signingCredentials: creds
			//    );

			//var jwtHandler = new JwtSecurityTokenHandler().WriteToken(token);

			//return jwtHandler;
        }
    }
}
