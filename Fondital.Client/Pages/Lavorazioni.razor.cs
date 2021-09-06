using Fondital.Shared.Models;
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
        private List<Lavorazione> ListaLavorazioni;
        private int PageSize { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        protected Lavorazione LavorazioneSelected { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PageSize = Convert.ToInt32(config["PageSize"]);
            await RefreshLavorazioni();
        }

        protected async Task RefreshLavorazioni()
        {
            ListaLavorazioni = (List<Lavorazione>)await httpClient.GetAllLavorazioni();
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
