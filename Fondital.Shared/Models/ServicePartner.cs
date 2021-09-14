using Fondital.Shared.Models.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Models
{
    public class ServicePartner
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
        public List<Utente> Utenti { get; set; } = new List<Utente>();
        public List<Listino> Listini { get; set; } = new List<Listino>();
    }
}
