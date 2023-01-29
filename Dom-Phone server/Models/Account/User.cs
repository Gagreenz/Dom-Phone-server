namespace Dom_Phone_server.Models.Account
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;

    }
}
