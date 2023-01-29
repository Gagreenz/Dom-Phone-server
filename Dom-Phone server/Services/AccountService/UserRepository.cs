using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Models.DB;
using Dom_Phone_server.Models.RepositoryData;
using Dom_Phone_server.Services.AccountService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Threading.Tasks;

namespace Dom_Phone_server.Services.AccountService
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<UserRepositoryResponse<IEnumerable<User>>> GetAsync(int count = 10)
        {
            IEnumerable<User> users = await _userContext.Users.Take(count).ToListAsync();

            return UserRepositoryResponse<IEnumerable<User>>.CreateResponse(users, $"Can`t get {count} Users");
        }
        public async Task<UserRepositoryResponse<User>> GetByLoginAsync(string login)
        {
            User user = await _userContext.Users.Where(u => u.Login == login).FirstOrDefaultAsync();
            
            return UserRepositoryResponse<User>.CreateResponse(user, $"User with login:[{login}] not found.");
        }
        public async Task<UserRepositoryResponse<User>> GetByPhoneAsync(string phoneNumber)
        {
            User user = await _userContext.Users.Where(u => u.PhoneNumber == phoneNumber).FirstOrDefaultAsync();

            return UserRepositoryResponse<User>.CreateResponse(user, $"User with number:[{phoneNumber}] not found.");
        }
        public async Task<UserRepositoryResponse<User>> AddAsync(User user)
        {
            if (user == null) return UserRepositoryResponse<User>.CreateResponse(user, $"User can't be null.");

            var addedUser = _userContext.Users.Add(user).Entity;

            return UserRepositoryResponse<User>.CreateResponse(addedUser, $"User is not created."); 
        }
        public async Task<UserRepositoryResponse<User>> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
        public void DeleteById(int userId)
        {
            throw new NotImplementedException();
        }
        public void Delete(User user)
        {
            throw new NotImplementedException();
        }
        public Task SaveAsync()
        {
            return _userContext.SaveChangesAsync();
        }
    }
}
