using Dom_Phone_server.Dtos.User;
using Dom_Phone_server.Models;
using Dom_Phone_server.Models.Data;
using System.IdentityModel.Tokens.Jwt;

namespace Dom_Phone_server.Services.AccountService.Interfaces
{
    public interface IUserRepository
    {
        Task<ServiceResponse<User>> Register(UserRegisterDto userRegisterDto);
        Task<ServiceResponse<User>> Login(UserLoginDto userLoginDto);
        Task<ServiceResponse<User>> GetUserById(Guid id);
        Task<ServiceResponse<UserInfoDto>> GetInfo(Guid id);
        Task<ServiceResponse<bool>> Update(Guid id, UserUpdateDto userDto);
        Task SetRefreshToken(RefreshToken refreshToken);
        Task<bool> DeleteRefreshToken(JwtSecurityToken refreshToken);
        Task<bool> IsUserExist(string login, string phone);

    }
}