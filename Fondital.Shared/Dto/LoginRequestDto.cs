using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class LoginRequestDto
    {
        [Display(Name = "Email", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        [EmailAddress(ErrorMessageResourceName = "ValidEmail", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string Password { get; set; }
    }
}