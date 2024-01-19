using MainTz.Application.Models.OptionsModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using MainTz.Application.Services;
using System.Security.Claims;
using System.Text;

namespace MainTz.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthOptions _authOptions;
        public TokenService(IOptions<JwtAuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
        }

        public string CreateAccessToken(string role, string name)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, name)
                };

            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_authOptions.Key)),
                        SecurityAlgorithms.HmacSha256)
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string CreateRefreshToken(string role, string name)
        {
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, name)
            };
            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(3)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_authOptions.Key)),
                        SecurityAlgorithms.HmacSha256)
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
