using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class Index
    {
        private List<string> attributi = new List<string>();
        Utente utente => authState.utente;

        private async Task GetClaimsPrincipalData()
        {
            attributi.Clear();

            if (utente != null)
            {
                attributi.Add($"{utente.UserName} è autenticato.");
                attributi.Add($"Nome: {utente.Nome}");
                attributi.Add($"Cognome: {utente.Cognome}");
            }
            else
            {
                attributi.Add("Non hai effettuato il login.");
            }
        }
    }
}
