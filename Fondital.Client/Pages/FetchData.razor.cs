using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class FetchData
    {
        private List<Utente> utenti;

        protected override async Task OnInitializedAsync()
        {
            await RefreshUtenti();
        }

        //protected async void chiamaCreazioneTrace()
        //{
        //    await Http.CreateDummyTrace($"Trace del giorno {DateTime.Now.ToShortDateString()} alle ore {DateTime.Now.ToShortTimeString()}");
        //    await RefreshTraces();
        //}

        protected async Task RefreshUtenti()
        {
            utenti = (List<Utente>)await Http.GetUtenti();
            StateHasChanged();
        }
    }
}
