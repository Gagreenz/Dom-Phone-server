using Dom_Phone_server.Models;
using Dom_Phone_server.Models.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dom_Phone_server.Services.TokenService.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public RefreshToken GenerateRefreshToken(User user);
        public JwtSecurityToken GetJwt(string token);
        public bool VerifyRefreshToken(User user, string refreshToken);
    }
}
