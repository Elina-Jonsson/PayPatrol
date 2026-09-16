using System.ComponentModel.DataAnnotations;

namespace PayPatrol.Application.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;
    }
}
