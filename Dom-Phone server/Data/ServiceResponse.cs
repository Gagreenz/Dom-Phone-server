namespace Dom_Phone_server.Models.Data
{
    public class ServiceResponse<T>
        where T : class
    {
        public bool IsSuccess { get; set; } = true;
        public T? Data { get; set; } = default(T?);
        public string Message { get; set; } = string.Empty;
    }
}