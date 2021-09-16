using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class ChangePwRequestDto
    {
        [Display(Name = "OldPassword", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string OldPassword { get; set; }
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        //[RegularExpression("pattern", ErrorMessageResourceName = "PasswordValidation", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string NewPassword { get; set; }
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
    }
}