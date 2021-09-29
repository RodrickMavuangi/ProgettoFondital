using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddRapportoVoceCostoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public EventCallback<RapportoVoceCostoDto> NewRapportoVoceCostoChanged { get; set; }
        [Parameter] public RapportoVoceCostoDto NewRapportoVoceCosto { get; set; } = new();

        public async Task Done()
        {
            await InvokeAsync(() => NewRapportoVoceCostoChanged.InvokeAsync(NewRapportoVoceCosto));
            await OnSave.InvokeAsync();
        }
    }
}
