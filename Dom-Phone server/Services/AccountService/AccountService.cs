using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Models.DB;
using Dom_Phone_server.Services.AccountService.Interfaces;
using System.Threading.Tasks;

namespace Dom_Phone_server.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepo;
        public AccountService(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }
        public User Login()
        {
            var r = _userRepo.GetUsers(2);


            return null;
        }
        public User Register(UserRegisterDto user)
        {
            if (user == null) return null;

            var login = _userRepo.GetUserByLogin(user.Login);
            var phone = _userRepo.GetUserByPhone(user.Phone);

            //If login or phone exist should return
            if (login.IsSuccess || phone.IsSuccess)
            {
                return null;
            }

            var response =  _userRepo.AddUser(new User()
            {
                Login = user.Login,
                PhoneNumber = user.Phone,
                PasswordHash = user.Password
            });

            _userRepo.Save();

            return response.Data;
        }



    }
}
