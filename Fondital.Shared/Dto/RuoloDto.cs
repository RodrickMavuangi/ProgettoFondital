using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Fondital.Shared.Dto
{
    public class RuoloDto : IdentityRole<int>
    {
        public List<Utente> Utenti { get; set; } = new List<Utente>();
    }
}
