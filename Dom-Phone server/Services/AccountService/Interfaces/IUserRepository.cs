using Dom_Phone_server.Models.Account;

namespace Dom_Phone_server.Services.AccountService.Interfaces
{
    public interface IUserRepository
    {
        public UserRepositoryResponse<IEnumerable<User>> GetUsers(int count);
        public UserRepositoryResponse<User> GetUserByLogin(string login);
        public UserRepositoryResponse<User> GetUserByPhone(string phoneNumber);
        public UserRepositoryResponse<User> AddUser(User user);
        public UserRepositoryResponse<User> UpdateUser(User user);
        public void DeleteUser(int userId);
        public void DeleteUser(User user);
        public void Save();
        
    }
}