using MainTz.Application.Models.OptionsModels;
using MainTz.Application.Models.AuthModels;
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

        public AuthTokensModel CreateNewTokensModel(string role, string name, int id)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, name),
                new Claim("Id", id.ToString())
            };
            var jwtAccess = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_authOptions.Key)),
                        SecurityAlgorithms.HmacSha256)
                    );
            var jwtRefresh = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(3)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_authOptions.Key)),
                        SecurityAlgorithms.HmacSha256)
                    );
            var authTokenModel = new AuthTokensModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtAccess),
                RefreshToken = new JwtSecurityTokenHandler().WriteToken(jwtRefresh),
                Role = role,
                UserId = id,
                UserName = name
            };
            return authTokenModel;
        }
        public bool CheckHealthToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var resultToken = handler.ReadJwtToken(token);
            if(resultToken.ValidTo < DateTime.Now)
                return false;

            return true;
        }
    }
}
