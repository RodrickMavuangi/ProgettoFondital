using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Resources
{
    public class ChangePwRequest
    {
        [Required]        
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
    }
}