using Dom_Phone_server.Models;
using Dom_Phone_server.Models.Data;

namespace Dom_Phone_server.Services.TokenService.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken(User user);
        public Guid GetId(string token);
        public bool VerifyRefreshToken(string refreshToken, User user);
    }
}
