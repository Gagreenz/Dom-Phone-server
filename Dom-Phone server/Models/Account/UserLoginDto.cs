using System.ComponentModel.DataAnnotations;

namespace Dom_Phone_server.Models.Account
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(20)]
        public string Login { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")]
        public string Password { get; set; } = string.Empty;
    }
}
