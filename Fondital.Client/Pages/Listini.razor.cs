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
    public partial class Listini
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }

        private List<ListinoDto> ListaListini;
        private int PageSize { get; set; }
        private string CurrentCulture { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        protected ListinoDto ListinoSelected { get; set; }

        public string SearchSPText = "";
        public string SearchVoceCostoText = "";
        public string SearchRaggruppamentoText = "";

        protected override async Task OnInitializedAsync()
        {
            var js = (IJSInProcessRuntime)JSRuntime;
            CurrentCulture = await js.InvokeAsync<string>("blazorCulture.get");
            PageSize = Convert.ToInt32(config["PageSize"]);

            await RefreshListini();
        }

        public List<ListinoDto> ListiniFiltered =>
            ListaListini.Where(x => x.ServicePartner.RagioneSociale.Contains(SearchSPText, StringComparison.InvariantCultureIgnoreCase)
            && x.Raggruppamento.Contains(SearchRaggruppamentoText, StringComparison.InvariantCultureIgnoreCase) 
            &&  (
                    (CurrentCulture == "ru-RU" && x.VoceCosto.NomeRusso.Contains(SearchVoceCostoText, StringComparison.InvariantCultureIgnoreCase)) 
                    || (CurrentCulture == "it-IT" && x.VoceCosto.NomeItaliano.Contains(SearchVoceCostoText, StringComparison.InvariantCultureIgnoreCase)
                )
            )).ToList();

        protected async Task RefreshListini()
        {
            ListaListini = (List<ListinoDto>)await httpClient.GetAllListini();
            StateHasChanged();
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddDialog = false;
            ShowEditDialog = false;
            await RefreshListini();
        }

        protected void EditListino(int listinoId)
        {
            ListinoSelected = ListaListini.Single(x => x.Id == listinoId);
            ShowEditDialog = true;
        }
    }
}