using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fondital.Shared.Models.Auth;

namespace Fondital.Client.Shared
{
    public partial class LoginDisplay
    {
        async Task BeginSignOut()
        {
            await SignOutManager.SetSignOutState();
            authState.UtenteCorrente = new Utente();

            Navigation.NavigateTo("authentication/logout");
        }
    }
}
