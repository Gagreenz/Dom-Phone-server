using Dom_Phone_server.Models.Account;

namespace Dom_Phone_server.Models.RepositoryData
{
    public class UserRepositoryResponse<T>
        where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string ErrorMessage { get; set; }
        private UserRepositoryResponse(bool isSuccess, T data, string errorMessage = "")
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }
        public static UserRepositoryResponse<T> CreateResponse(T data, string errorMessage)
        {
            if (data == default!)
                return new UserRepositoryResponse<T>(false, default!, errorMessage);
            else
                return new UserRepositoryResponse<T>(true, data);
        }

    }
}