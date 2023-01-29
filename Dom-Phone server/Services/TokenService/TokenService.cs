using Dom_Phone_server.Models.Account;
using Microsoft.IdentityModel.Tokens;
using Dom_Phone_server.Models.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Dom_Phone_server.Services.TokenService.Interfaces;

namespace Dom_Phone_server.Services.TokenService
{
    public class TokenService : ITokenService
    {
        IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //TODO: add more claims variety and set parameters for claims
        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                new Claim(ClaimTypes.Role,"Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Security:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken()
            {
                Token = GenerateToken(user),
                CreatedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddDays(30)
            };
            return refreshToken;
        }
    }
}
