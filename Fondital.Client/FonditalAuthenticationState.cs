using Fondital.Shared.Enums;
using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fondital.Client
{
    public class FonditalAuthenticationState : RemoteAuthenticationState
    {
        public Utente UtenteCorrente { get; set; }
        public Lingua CurrentLang { get; set; } = Lingua.ITA;
    }
}
