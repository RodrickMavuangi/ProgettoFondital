using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class DetailRapporto
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        private RapportoDto Rapporto { get; set; } = new();
        private List<RapportoVoceCostoDto> RapportiVociCosto { get; set; } = new();
        private RapportoVoceCostoDto NewRapportoVoceCosto { get; set; } = new();
        private int CurrentStepIndex { get; set; }
        private string CurrentCulture { get; set; }
        private bool ShowAddVoceCosto { get; set; }
        [Parameter] public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ShowAddVoceCosto = false;

            var js = (IJSInProcessRuntime)JSRuntime;
            CurrentCulture = await js.InvokeAsync<string>("blazorCulture.get");

            var authState = await AuthStateTask;
            if (authState.User.Identity.IsAuthenticated)
                Rapporto.Utente = await utenteHttpClient.GetUtente(authState.User.Identity.Name);
        }

        protected void PreviousStep()
        {
            if (CurrentStepIndex > 0)
                CurrentStepIndex--;
        }

        protected void NextStep()
        {
            if (CurrentStepIndex < 3)
                CurrentStepIndex++;
        }

        protected void NuovoRicambio()
        {
            Rapporto.Ricambi.Add(new RicambioDto());
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddVoceCosto = false;
            await InvokeAsync(StateHasChanged);
        }

        protected async Task Salva()
        {
            //Rapporto.VociDiCosto.AddRange((IEnumerable<RapportoVoceCostoDto>)Enumerable.Repeat(NewRapportoVoceCosto.VoceCosto, NewRapportoVoceCosto.Quantita));
            RapportiVociCosto.Add(NewRapportoVoceCosto);
            await CloseAndRefresh();
        }

        protected void EditVoceCosto(int Id)
        {
        }
    }
}
