using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
            var js = (IJSInProcessRuntime)JSRuntime;
            CurrentCulture = await js.InvokeAsync<string>("blazorCulture.get");
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
            bool isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaModificaLavorazione"]} {Id}", localizer["ModificaLavorazione"]);

            if (isConfirmed)
            {
                try
                {
                    await httpClient.UpdateLavorazione(Id, ListaLavorazioni.Single(x => x.Id == Id));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                ListaLavorazioni.Single(x => x.Id == Id).IsAbilitato ^= true;
            }
        }
    }
}
