using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        private List<VoceCosto> ListaVociCosto;
        private int PageSize { get; set; }
        private string CurrentCulture { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        protected VoceCosto VoceCostoSelected { get; set; }

        public string SearchText = "";

        protected override async Task OnInitializedAsync()
        {
            var js = (IJSInProcessRuntime)JSRuntime;
            CurrentCulture = await js.InvokeAsync<string>("blazorCulture.get");
            PageSize = Convert.ToInt32(config["PageSize"]);

            await RefreshVociCosto();
        }
        public List<VoceCosto> ListaLavorazioni_filtered => CurrentCulture == "ru-RU" ?
            ListaVociCosto.Where<VoceCosto>(x => x.NomeRusso.ToLower().Contains(SearchText.ToLower())).ToList() :
            ListaVociCosto.Where<VoceCosto>(x => x.NomeItaliano.ToLower().Contains(SearchText.ToLower())).ToList();

        protected async Task RefreshVociCosto()
        {
            ListaVociCosto = (List<VoceCosto>)await httpClient.GetAllVociCosto();
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
            bool isConfirmed = await Dialogs.ConfirmAsync($"Si è sicuri di voler modificare la voce di costo # {Id}?", "Modifica voce di costo");

            if (isConfirmed)
            {
                try
                {
                    await httpClient.UpdateVoceCosto(Id, ListaVociCosto.Single(x => x.Id == Id));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                //fai revert: ^ restituisce lo XOR dei due valori
                //true XOR true = false
                //false XOR true = true
                ListaVociCosto.Single(x => x.Id == Id).IsAbilitato ^= true;
            }
        }
    }
}
