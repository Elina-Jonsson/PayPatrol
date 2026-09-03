using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PayPatrol.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
    }
}
