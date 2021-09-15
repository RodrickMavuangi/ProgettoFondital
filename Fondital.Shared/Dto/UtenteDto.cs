using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class UtenteDto : IdentityUser<int>
    {
        public ServicePartnerDto ServicePartner { get; set; }
        public bool IsAbilitato { get; set; } = true;

        [Required(ErrorMessage = "CampoObbligatorio")]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "CampoObbligatorio")]
        public string Cognome { get; set; } = "";
        public DateTime Pw_LastChanged { get; set; } = DateTime.Now;
        public bool Pw_MustChange { get; set; } = true;
    }
}
