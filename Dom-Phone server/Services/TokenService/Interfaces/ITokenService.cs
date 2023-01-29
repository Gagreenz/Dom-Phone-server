using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Models.Data;

namespace Dom_Phone_server.Services.TokenService.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public RefreshToken GenerateRefreshToken(User user);
    }
}
