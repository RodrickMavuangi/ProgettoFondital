using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class FetchUtenti
    {
        private List<Utente> utenti;

        protected override async Task OnInitializedAsync()
        {
            await RefreshUtenti();
        }

        protected async Task RefreshUtenti()
        {
            utenti = (List<Utente>)await Http.GetUtenti();
            StateHasChanged();
        }
    }
}
