using Dom_Phone_server.Models.Account;

namespace Dom_Phone_server.Services.AccountService.Interfaces
{
    public interface IAccountService
    {
        public User Register(UserRegisterDto user);
        public User Login();
    }
}
