using System;
using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class RicambioRequestDto
    {
        [Display(Name = "IdRicambio", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        [Range(0, Int32.MaxValue, ErrorMessageResourceName = "OutOfBounds", ErrorMessageResourceType = typeof(Resources.Validation))]
        public int Id { get; set; }

        [Display(Name = "IdFornitore", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        [Range(0, Int32.MaxValue, ErrorMessageResourceName = "OutOfBounds", ErrorMessageResourceType = typeof(Resources.Validation))]
        public int SupplierId { get; set; }

        [Display(Name = "Quantita", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        [Range(0, Int32.MaxValue, ErrorMessageResourceName = "OutOfBounds", ErrorMessageResourceType = typeof(Resources.Validation))]
        public int Quantity { get; set; }
    }
}
