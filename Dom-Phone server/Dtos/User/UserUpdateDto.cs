namespace Dom_Phone_server.Dtos.User
{
    public class UserUpdateDto
    {
        public string? Login { get; set; } = string.Empty;
        public byte[]? Img { get; set; } = null;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Password { get; set; } = default!;
    }
}
