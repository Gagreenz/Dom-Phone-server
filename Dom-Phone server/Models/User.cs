using Dom_Phone_server.Models.Data;

namespace Dom_Phone_server.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; } = string.Empty;
        public byte[]? Img { get; set; } = default!;
        public string PhoneNumber { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
        public List<RefreshToken> RefreshTokens { get; set; } = new();
        public List<Payment> payments { get; set; } = new();
    }
}
