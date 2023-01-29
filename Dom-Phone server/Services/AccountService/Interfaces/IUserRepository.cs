using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Models.RepositoryData;

namespace Dom_Phone_server.Services.AccountService.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserRepositoryResponse<IEnumerable<User>>> GetAsync(int count);
        public Task<UserRepositoryResponse<User>> GetByLoginAsync(string login);
        public Task<UserRepositoryResponse<User>> GetByPhoneAsync(string phoneNumber);
        public Task<UserRepositoryResponse<User>> AddAsync(User user);
        public Task<UserRepositoryResponse<User>> UpdateAsync(User user);
        public void DeleteById(int userId);
        public void Delete(User user);
        public Task SaveAsync();
        
    }
}