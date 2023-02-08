using Dom_Phone_server.Dtos.User;
using Dom_Phone_server.Models;
using Dom_Phone_server.Models.Data;
using Dom_Phone_server.Models.DB;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Dom_Phone_server.Services.TokenService;
using Dom_Phone_server.Services.TokenService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Dom_Phone_server.Services.AccountService
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext userContext)
        {
            _context = userContext;
        }

        public async Task<ServiceResponse<User>> Login(UserLoginDto userLoginDto)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();

            User user = await _context.Users.SingleOrDefaultAsync(u => u.Login == userLoginDto.Login);
            if (user == null)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"User {userLoginDto.Login} not found.";

                return response;
            }

            if (!IsPasswordEqual(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "Wrong password.";
            }

            response.Data = user;
            return response;
        }
        public async Task<ServiceResponse<User>> Register(UserRegisterDto userRegisterDto)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            if (await IsUserExist(userRegisterDto.Login, userRegisterDto.Phone))
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "User already exists.";

                return response;
            }

            CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            EntityEntry<User> entityEntry = await _context.AddAsync(new User
            {
                Login = userRegisterDto.Login,
                PhoneNumber = userRegisterDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            await _context.SaveChangesAsync();

            response.Data = entityEntry.Entity;
            return response;
        }
        public async Task<bool> IsUserExist(string login, string phone)
        {
            if(await _context.Users.AnyAsync(u => u.Login == login || u.PhoneNumber == phone))
            {
                return true;
            }
            return false;
        }
        public async Task<ServiceResponse<User>> GetUserById(Guid id)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if(user == null)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"User with id:{id} not found.";

                return response;
            }

            response.Data = user;
            return response;
         }
        public async Task SetRefreshToken(RefreshToken refreshToken)
        { 
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteRefreshToken(JwtSecurityToken jwt)
        {
            var userId = Guid.Parse(jwt.Claims.First(c => c.Type == "UserId").Value);
            var serviceResponse = await GetUserById(userId);
            if (!serviceResponse.IsSuccess) return false;

            var jwtId = jwt.Claims.First(c => c.Type == "Id").Value;
            var token = _context.RefreshTokens.FirstOrDefault(rt => rt.Id == Guid.Parse(jwtId));
            if(token == null) return false;

            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync();

            return true;
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
