using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Resources
{
    public class LoginRequest
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
