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
        protected string CurrentCulture { get; set; }
        protected bool IsButtonEnabled =>
            (RapportoVoceCosto.VoceCosto.Tipologia == TipologiaVoceCosto.Quantita && RapportoVoceCosto.Quantita > 0) || RapportoVoceCosto.VoceCosto.Tipologia == TipologiaVoceCosto.Forfettario;

        protected override async Task OnInitializedAsync()
        {
            CurrentCulture = await StateProvider.GetCurrentCulture();
        }

        public async Task Done()
        {
            await RapportoVoceCostoChanged.InvokeAsync(RapportoVoceCosto);
            await OnSave.InvokeAsync();
        }
    }
}
