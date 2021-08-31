using Fondital.Shared.Enums;
using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Fondital.Client
{
    public class FonditalAuthenticationState : RemoteAuthenticationState
    {
        public Utente UtenteCorrente { get; set; }
        public Lingua CurrentLang { get; set; } = Lingua.IT;
    }
}
