using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor;

namespace Fondital.Client.Pages
{
    public partial class Lavorazioni
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }

        private List<LavorazioneDto> ListaLavorazioni;
        private int PageSize { get; set; }
        private string CurrentCulture { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        protected LavorazioneDto LavorazioneSelected { get; set; }
        public string SearchText = "";

        protected override async Task OnInitializedAsync()
        {
            CurrentCulture = await StateProvider.GetCurrentCulture();
            PageSize = Convert.ToInt32(config["PageSize"]);

            await RefreshLavorazioni();
        }

        public List<LavorazioneDto> ListaLavorazioniFiltered => CurrentCulture == "ru-RU" ?
            ListaLavorazioni.Where(x => x.NomeRusso.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList() :
            ListaLavorazioni.Where(x => x.NomeItaliano.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();

        protected async Task RefreshLavorazioni()
        {
            ListaLavorazioni = (List<LavorazioneDto>)await httpClient.GetAllLavorazioni();
            StateHasChanged();
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddDialog = false;
            ShowEditDialog = false;
            await RefreshLavorazioni();
        }

        protected void EditLavorazione(int lavorazioneId)
        {
            LavorazioneSelected = ListaLavorazioni.Single(x => x.Id == lavorazioneId);
            ShowEditDialog = true;
        }

        protected async Task UpdateEnableLavorazione(int Id)
        {
            bool isAbilitato = ListaLavorazioniFiltered.Single(x => x.Id == Id).IsAbilitato;
            bool isConfirmed = false;
            if (isAbilitato) isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaAbilitazione"]} {localizer["Lavorazione"]} # {Id}", " ");
            else isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaDisabilitazione"]} {localizer["Lavorazione"]} # {Id}", " ");

            if (isConfirmed)
            {
                try
                {
                    await httpClient.UpdateLavorazione(Id, ListaLavorazioniFiltered.Single(x => x.Id == Id));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                ListaLavorazioniFiltered.Single(x => x.Id == Id).IsAbilitato ^= true;
            }
        }
    }
}