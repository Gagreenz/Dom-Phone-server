using Dom_Phone_server.Models.Data;

namespace Dom_Phone_server.Models.Account
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
        public RefreshToken? RefreshToken { get; set; }
    }
}
