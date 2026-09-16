using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PayPatrol.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
    }
}
