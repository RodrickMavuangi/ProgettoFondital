using System;
using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class ListinoDto
    {
        public int Id { get; set; }

        [Display(Name = "ServicePartner", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public ServicePartnerDto ServicePartner { get; set; }

        [Display(Name = "VoceCosto", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public VoceCostoDto VoceCosto { get; set; }

        [Display(Name = "Raggruppamento", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string Raggruppamento { get; set; } = "";

        [Display(Name = "Valore", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        [Range(0, Int32.MaxValue, ErrorMessageResourceName = "OutOfBounds", ErrorMessageResourceType = typeof(Resources.Validation))]
        public int Valore { get; set; } = 0;
    }
}
