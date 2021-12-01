using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Fondital.Shared.Dto
{
    public class ServicePartnerRequestDto
    {

        [Display(Name = "CodiceFornitore", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string CodiceFornitore { get; set; } 

    }
}