using Fondital.Client.Clients;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telerik.Blazor;

namespace Fondital.Client.Pages
{
    public partial class Difetti
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        private List<Difetto> ListaDifetti;
        private int PageSize { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        protected Difetto DifettoSelected { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PageSize = Convert.ToInt32(config["PageSize"]);
            await RefreshDifetti();
        }

        protected async Task RefreshDifetti()
        {
            ListaDifetti = (List<Difetto>)await httpClient.GetAllDifetti();
            StateHasChanged();
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddDialog = false;
            ShowEditDialog = false;
            await RefreshDifetti();
        }

        protected void EditDifetto(int difettoId)
        {
            DifettoSelected = ListaDifetti.Single(x => x.Id == difettoId);
            ShowEditDialog = true;
        }

        protected async Task UpdateEnableDifetto(int Id)
        {
            bool isConfirmed = await Dialogs.ConfirmAsync($"Sicuri di voler modificare il difetto # {Id}?", "Modifica difetto");

            if (isConfirmed)
            {
                try
                {
                    await httpClient.UpdateDifetto(Id, ListaDifetti.Single(x => x.Id == Id));
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
                ListaDifetti.Single(x => x.Id == Id).IsAbilitato ^= true;
            }
        }
    }
}
