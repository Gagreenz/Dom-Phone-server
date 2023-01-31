using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Dom_Phone_server.Services.TokenService.Interfaces;
using Dom_Phone_server.Models;
using Dom_Phone_server.Models.DB;

namespace Dom_Phone_server.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //TODO: add more claims variety and set parameters for claims
        public string GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Expired,DateTime.Now.AddMinutes(30).ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Security:AccessKey").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public string GenerateRefreshToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Expired,DateTime.Now.AddDays(30).ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Security:RefreshKey").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public Guid GetId(string token) 
        {
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string id = jwt.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return Guid.Parse(id);
            }
            catch
            {
                return Guid.Empty;
            }
            
        }
        public bool VerifyRefreshToken(string refreshToken, User user)
        { 
            if (user.RefreshToken == null || user.RefreshToken != refreshToken) return false;

            return true;
        }
    }
}
