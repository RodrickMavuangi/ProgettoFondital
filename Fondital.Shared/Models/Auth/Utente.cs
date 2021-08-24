using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fondital.Shared.Models.Auth
{
    public class Utente : IdentityUser<int>
    {
        public ServicePartner ServicePartner { get; set; }
        public bool IsAbilitato { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime Pw_LastChanged { get; set; }
        public bool Pw_MustChange { get; set; }
    }
}
