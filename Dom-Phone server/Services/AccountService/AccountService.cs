using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Models.Data;
using Dom_Phone_server.Models.DB;
using Dom_Phone_server.Services.AccountService.Interfaces;
using System.Security.Cryptography;
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
        public async Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null) return null;

            var repositoryResponse = await _userRepo.GetByLoginAsync(userLoginDto.Login);

            //Todo: add response with errors
            if (!repositoryResponse.IsSuccess) return null;
            
            if (IsPasswordEqual(userLoginDto.Password, repositoryResponse.Data.PasswordHash, repositoryResponse.Data.PasswordSalt))
                return repositoryResponse.Data;

            return null;
        }

        public async Task<User> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null) return null;

            var loginResponse = await _userRepo.GetByLoginAsync(userRegisterDto.Login);
            var phoneResponse = await _userRepo.GetByPhoneAsync(userRegisterDto.Phone);

            //If login or phone exist should return
            //Todo: add response with errors
            if (loginResponse.IsSuccess || phoneResponse.IsSuccess)
            {
                return null;
            }

            CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var addResponse = await _userRepo.AddAsync(new User()
            {
                Login = userRegisterDto.Login,
                PhoneNumber = userRegisterDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            //Todo: add response with errors
            if (!addResponse.IsSuccess) return null;

            await _userRepo.SaveAsync();

            return addResponse.Data;
        }

        public void SetRefreshToken(RefreshToken refreshToken, User user)
        {
            user.RefreshToken = refreshToken;

            _userRepo.UpdateAsync(user);
            _userRepo.SaveAsync();
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool IsPasswordEqual(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var temp = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash.SequenceEqual(temp);
            }
        }
    }
}
