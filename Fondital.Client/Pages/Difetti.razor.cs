using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
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
            CurrentCulture = await StateProvider.GetCurrentCulture();
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
            bool isAbilitato = ListaDifettiFiltered.Single(x => x.Id == Id).IsAbilitato;
            bool isConfirmed = false;
            if (isAbilitato) isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaAbilitazione"]} {localizer["Difetto"]} # {Id}", " ");
            else isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaDisabilitazione"]} {localizer["Difetto"]} # {Id}", " ");

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