using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class ResetPwRequestDto
    {
        [Display(Name = "Password", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        //[RegularExpression("pattern", ErrorMessageResourceName = "PasswordValidation", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}