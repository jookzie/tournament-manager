using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class UserRegister
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name can only contain letters.")]
        [MinLength(2, ErrorMessage = "Length must be at least 2 characters.")]
        [MaxLength(64, ErrorMessage = "Maximum length is 64 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Length must be at least 8 characters.")]
        [MaxLength(64, ErrorMessage = "Password maximum length is 64 characters.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The fields 'Password' and 'Confirm password' do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
