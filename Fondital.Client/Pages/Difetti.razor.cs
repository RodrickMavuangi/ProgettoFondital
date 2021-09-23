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
    public partial class Difetti
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }

        private List<DifettoDto> ListaDifetti;
        private int PageSize { get; set; }
        private string CurrentCulture { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        protected DifettoDto DifettoSelected { get; set; }

        public string SearchText = "";

        protected override async Task OnInitializedAsync()
        {
            var js = (IJSInProcessRuntime)JSRuntime;
            CurrentCulture = await js.InvokeAsync<string>("blazorCulture.get");
            PageSize = Convert.ToInt32(config["PageSize"]);

            await RefreshDifetti();
        }

        public List<DifettoDto> ListaDifettiFiltered => CurrentCulture == "ru-RU" ?
            ListaDifetti.Where(x => x.NomeRusso.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList() :
            ListaDifetti.Where(x => x.NomeItaliano.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();

        protected async Task RefreshDifetti()
        {
            ListaDifetti = (List<DifettoDto>)await httpClient.GetAllDifetti();
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
            bool isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaModificaDifetto"]} {Id}", localizer["Modifica"] + " " + localizer["Difetto"]);

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
