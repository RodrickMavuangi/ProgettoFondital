using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class Authentication
    {
        [Parameter] public string Action { get; set; }

        public FonditalAuthenticationState authState { get; set; } = new FonditalAuthenticationState();

        protected override void OnInitialized()
        {
            if (RemoteAuthenticationActions.IsAction(RemoteAuthenticationActions.LogIn, Action))
            {
                //preserve previous state (non dovrebbe servire in quanto il login viene effettuato come prima cosa)
            }
        }

        private async Task RestoreState(FonditalAuthenticationState appState)
        {
            //restore state
            var _authState = await authStateProvider.GetAuthenticationStateAsync();
            if (_authState.User.Identity.Name != null)
                applicationState.utente = await utenteClient.GetUtente(_authState.User.Identity.Name);
            else
                applicationState.utente = new Utente();
        }
    }
}
