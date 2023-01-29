using Dom_Phone_server.Models.Account;
using Dom_Phone_server.Models.DB;
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
        public UserRepositoryResponse<IEnumerable<User>> GetUsers(int count = 10)
        {
            IEnumerable<User> users = _userContext.Users.Take(count).ToList();

            return UserRepositoryResponse<IEnumerable<User>>.CreateResponse(users, $"Can`t get {count} Users");
        }
        public UserRepositoryResponse<User> GetUserByLogin(string login)
        {
            User user = _userContext.Users.Where(u => u.Login == login).FirstOrDefault();
            
            return UserRepositoryResponse<User>.CreateResponse(user, $"User with number:[{login}] not found.");
        }
        public  UserRepositoryResponse<User> GetUserByPhone(string phoneNumber)
        {
            User user = _userContext.Users.Where(u => u.PhoneNumber == phoneNumber).FirstOrDefault();
            return UserRepositoryResponse<User>.CreateResponse(user, $"User with number:[{phoneNumber}] not found.");
        }
        public UserRepositoryResponse<User> AddUser(User user)
        {
            if (user == null) return UserRepositoryResponse<User>.CreateResponse(user, $"User can't be null.");

            var addedUser = _userContext.Users.Add(user).Entity;

            return UserRepositoryResponse<User>.CreateResponse(addedUser, $"User is not created."); 
        }
        public UserRepositoryResponse<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            _userContext.SaveChanges();
        }
    }
}
