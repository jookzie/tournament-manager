using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class UserAuth
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Length must be at least 8 characters.")]
        [MaxLength(64, ErrorMessage = "Password maximum length is 64 characters.")]
        public string Password { get; set; }
    }
}
