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
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, roles.ToString())
				};

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_authSettings.SecretKey)
                    ),
                    SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                _authSettings.Issuer,
                _authSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
                );

            var jwtHandler = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtHandler;
        }
    }
}
