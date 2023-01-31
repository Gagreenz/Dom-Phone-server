using System.ComponentModel.DataAnnotations;

namespace Dom_Phone_server.Dtos.User
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(20)]
        public string Login { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")]
        public string Password { get; set; } = string.Empty;
    }
}
