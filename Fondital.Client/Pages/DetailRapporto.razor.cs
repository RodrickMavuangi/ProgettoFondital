using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class DetailRapporto
    {
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
            CurrentCulture = await StateProvider.GetCurrentCulture();
            Rapporto.Utente = await StateProvider.GetCurrentUser();
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
            RapportiVociCosto.Add(NewRapportoVoceCosto);
            await CloseAndRefresh();
        }

        protected void EditVoceCosto(int Id)
        {
        }

        public string Modello => RestClient.ModelloCaldaiaService().ToString();
    }
}