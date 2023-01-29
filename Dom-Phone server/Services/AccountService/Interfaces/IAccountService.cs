using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Models.Data;

namespace Dom_Phone_server.Services.AccountService.Interfaces
{
    public interface IAccountService
    {
        public Task<User> RegisterAsync(UserRegisterDto userRegisterDto);
        public Task<User> LoginAsync(UserLoginDto userLoginDto);
        public void SetRefreshToken(RefreshToken refreshToken, User user);
    }
}
