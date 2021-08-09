using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Shared.Models.Auth
{
    public class Utente : IdentityUser<int>
    {
        public int SP_Id { get; set; }
        public bool IsAbilitato { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}
