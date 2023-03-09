using System.ComponentModel.DataAnnotations;

namespace Dom_Phone_server.Dtos.User
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(20)]
        public string Login { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
