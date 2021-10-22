using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor;

namespace Fondital.Client.Pages
{
    public partial class VociCosto
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        private List<VoceCostoDto> ListaVociCosto;
        private int PageSize { get; set; }
        private string CurrentCulture { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        protected VoceCostoDto VoceCostoSelected { get; set; }
        public string SearchText = "";

        protected override async Task OnInitializedAsync()
        {
            CurrentCulture = await StateProvider.GetCurrentCulture();
            PageSize = Convert.ToInt32(config["PageSize"]);

            await RefreshVociCosto();
        }
        public List<VoceCostoDto> ListaVociCostoFiltered => CurrentCulture == "ru-RU" ?
            ListaVociCosto.Where(x => x.NomeRusso.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList() :
            ListaVociCosto.Where(x => x.NomeItaliano.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();

        protected async Task RefreshVociCosto()
        {
            ListaVociCosto = (List<VoceCostoDto>)await httpClient.GetAllVociCosto();
            StateHasChanged();
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddDialog = false;
            ShowEditDialog = false;
            await RefreshVociCosto();
        }

        protected void EditVoceCosto(int voceCostoId)
        {
            VoceCostoSelected = ListaVociCosto.Single(x => x.Id == voceCostoId);
            ShowEditDialog = true;
        }

        protected async Task UpdateEnableVoceCosto(int Id)
        {
            bool isAbilitato = ListaVociCostoFiltered.Single(x => x.Id == Id).IsAbilitato;
            bool isConfirmed = false;
            if (isAbilitato) isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaAbilitazione"]} {localizer["VoceCosto"]} # {Id}", " ");
            else isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaDisabilitazione"]} {localizer["VoceCosto"]} # {Id}", " ");

            if (isConfirmed)
            {
                try
                {
                    await httpClient.UpdateVoceCosto(Id, ListaVociCostoFiltered.Single(x => x.Id == Id));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                ListaVociCostoFiltered.Single(x => x.Id == Id).IsAbilitato ^= true;
            }
        }
    }
}