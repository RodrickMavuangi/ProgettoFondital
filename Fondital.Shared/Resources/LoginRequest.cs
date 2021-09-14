using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Resources
{
    public class LoginRequest
    {
        [Required]        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}