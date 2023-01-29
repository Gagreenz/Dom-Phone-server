namespace Dom_Phone_server.Models.Account
{
    public class UserRegisterDto
    {
        public string Login { get; set; }  = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
