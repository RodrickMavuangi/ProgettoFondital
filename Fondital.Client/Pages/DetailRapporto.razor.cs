using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class DetailRapporto
    {
        private RapportoDto Rapporto { get; set; } = new();
        private List<RapportoVoceCostoDto> RapportiVociCosto { get; set; } = new();
        private RapportoVoceCostoDto NewRapportoVoceCosto { get; set; } = new();
        public List<LavorazioneDto> ListaLavorazioni { get; set; } = new();
        public List<string> LavorazioniDescription { get; set; } = new();
        public string Modello { get; set; }        
        private int CurrentStepIndex { get; set; }
        private string CurrentCulture { get; set; }
        private bool ShowAddVoceCosto { get; set; }
        [Parameter] public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ShowAddVoceCosto = false;
            CurrentCulture = await StateProvider.GetCurrentCulture();
            Rapporto.Utente = await StateProvider.GetCurrentUser();
            ListaLavorazioni = (List<LavorazioneDto>)await LavorazioneClient.GetAllLavorazioni(true);
            if (CurrentCulture == "it-IT")
                LavorazioniDescription = ListaLavorazioni.Select(x => x.NomeItaliano).ToList();
            else
                LavorazioniDescription = ListaLavorazioni.Select(x => x.NomeRusso).ToList();
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
            //if (CurrentStepIndex == 1)
                //GetModelloCaldaia();
        }

        protected void NuovoRicambio()
        {
            Rapporto.Ricambi.Add(new RicambioDto());
        }

        protected void RemoveRicambio(RicambioDto ricambio)
        {
            Rapporto.Ricambi.Remove(ricambio);
            CloseAndRefresh();
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddVoceCosto = false;
            await InvokeAsync(StateHasChanged);
        }

        protected async Task AggiungiVoceCosto()
        {
            RapportiVociCosto.Add(NewRapportoVoceCosto);
            await CloseAndRefresh();
        }

        protected async Task AggiungiRicambio()
        {
        }

        //protected async void GetModelloCaldaia() =>
        //    Modello = await RestClient.ModelloCaldaiaService(Rapporto.Caldaia.Matricola ?? "");

        protected void EditVoceCosto(int Id)
        {
        }
    }
}