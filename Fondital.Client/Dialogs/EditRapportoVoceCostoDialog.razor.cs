using Fondital.Shared.Dto;
using Fondital.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class EditRapportoVoceCostoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public EventCallback<RapportoVoceCostoDto> RapportoVoceCostoChanged { get; set; }
        [Parameter] public RapportoVoceCostoDto RapportoVoceCosto { get; set; }
        protected int QuantitaToEdit { get; set; }
        protected string CurrentCulture { get; set; }
        protected bool IsButtonEnabled =>
            (RapportoVoceCosto.VoceCosto.Tipologia == TipologiaVoceCosto.Quantita && QuantitaToEdit > 0) || RapportoVoceCosto.VoceCosto.Tipologia == TipologiaVoceCosto.Forfettario;

        protected override async Task OnInitializedAsync()
        {
            CurrentCulture = await StateProvider.GetCurrentCulture();
            QuantitaToEdit = RapportoVoceCosto.Quantita;
        }

        public async Task Done()
        {
            RapportoVoceCosto.Quantita = QuantitaToEdit;
            await RapportoVoceCostoChanged.InvokeAsync(RapportoVoceCosto);
            await OnSave.InvokeAsync();
        }
    }
}
