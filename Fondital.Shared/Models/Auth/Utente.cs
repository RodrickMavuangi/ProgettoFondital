using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fondital.Shared.Models.Auth
{
    public class Utente : IdentityUser<int>
    {
        public ServicePartner ServicePartner { get; set; }
        public bool IsAbilitato { get; set; } = true;
        [Required(ErrorMessage = "Campo Obbligatorio")]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "Campo Obbligatorio")]
        public string Cognome { get; set; } = "";
        public DateTime Pw_LastChanged { get; set; } = DateTime.Now;
        public bool Pw_MustChange { get; set; } = true;
    }
}
