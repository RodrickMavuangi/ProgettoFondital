using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class ServicePartnerDto
    {
        public int Id { get; set; }

        [Display(Name = "CodiceFornitore", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string CodiceFornitore { get; set; } = "";

        [Display(Name = "RagioneSociale", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string RagioneSociale { get; set; } = "";

        [Display(Name = "CodiceCliente", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string CodiceCliente { get; set; } = "";
        public List<UtenteDto> Utenti { get; set; } = new List<UtenteDto>();
        public List<ListinoDto> Listini { get; set; } = new List<ListinoDto>();
    }
}
