using Fondital.Shared.Dto;
using Fondital.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddRapportoVoceCostoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public EventCallback<RapportoDto> RapportoChanged { get; set; }
        [Parameter] public RapportoDto Rapporto { get; set; }
        protected List<VoceCostoDto> ListaVociCosto { get; set; } = new();
        protected List<string> ListaSelezione { get; set; } = new();
        protected string VoceCostoName { get; set; } = "";
        protected int Quantita { get; set; } = 1;
        protected string CurrentCulture { get; set; }
        protected VoceCostoDto VoceCostoToAdd =>
            ListaVociCosto.SingleOrDefault(x => (CurrentCulture == "it-IT" && x.NomeItaliano == VoceCostoName) || (CurrentCulture == "ru-RU" && x.NomeRusso == VoceCostoName));
        protected bool IsButtonEnabled =>
            VoceCostoToAdd != null && ((VoceCostoToAdd.Tipologia == TipologiaVoceCosto.Quantita && Quantita > 0) || VoceCostoToAdd.Tipologia == TipologiaVoceCosto.Forfettario);

        protected override async Task OnInitializedAsync()
        {
            CurrentCulture = await StateProvider.GetCurrentCulture();

            ListaVociCosto = (List<VoceCostoDto>)await voceCostoClient.GetAllVociCosto(true);

            if (CurrentCulture == "it-IT")
                ListaSelezione = ListaVociCosto.Where(x => !Rapporto.RapportiVociCosto.Select( y => y.VoceCostoId).Contains(x.Id)).Select(x => x.NomeItaliano).ToList();
            else
                ListaSelezione = ListaVociCosto.Where(x => !Rapporto.RapportiVociCosto.Select(y => y.VoceCostoId).Contains(x.Id)).Select(x => x.NomeRusso).ToList();
        }

        public async Task Done()
        {
            Rapporto.RapportiVociCosto.Add(new()
            {
                VoceCosto = VoceCostoToAdd,
                VoceCostoId = VoceCostoToAdd.Id,
                Rapporto = Rapporto,
                RapportoId = Rapporto.Id,
                Quantita = (VoceCostoToAdd.Tipologia == TipologiaVoceCosto.Quantita) ? Quantita : 1
            });
            await RapportoChanged.InvokeAsync(Rapporto);
            await OnSave.InvokeAsync();
        }       
    }
}