using Fondital.Shared.Dto;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class ListRapporti
    {
        private List<RapportoDto> ListaRapporti;
        private RapportoDto RapportoSelected { get; set; }
        private int PageSize { get; set; } = 10;
        private string CurrentCulture { get; set; }
        private string SearchBySp { get; set; } = "";
        private string SearchByStato { get; set; } = "";
        private string SearchByCliente { get; set; } = "";
        private string SearchById { get; set; }
        private string SearchByMatricola { get; set; } = "";
        private string SearchByTelefono { get; set; } = "";
        private string SearchByEmail { get; set; } = "";

        // Temporary for Data search input
        private string SearchText { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            //var js = (IJSInProcessRuntime)JSRuntime;
            //CurrentCulture = await js.InvokeAsync<string>("blazorCulture.get");
            PageSize = Convert.ToInt32(Config["PageSize"]);

            await RefreshRapporti();
        }

        // Missing date filter
        public List<RapportoDto> ListaRapporti_filtered => ListaRapporti
            .Where(x => x.ServicePartner.RagioneSociale.Contains(SearchBySp, StringComparison.InvariantCultureIgnoreCase) 
                     && x.Stato.ToString().Contains(SearchByStato, StringComparison.InvariantCultureIgnoreCase)
                     && x.Cliente.Contains(SearchByCliente, StringComparison.InvariantCultureIgnoreCase)
                     && x.Id.ToString().StartsWith(SearchById)
                     && x.Matricola.Contains(SearchByMatricola, StringComparison.InvariantCultureIgnoreCase)
                     && x.Telefono.Contains(SearchByTelefono, StringComparison.InvariantCultureIgnoreCase)
                     && x.Email.Contains(SearchByEmail, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        protected async Task RefreshRapporti()
        {
            ListaRapporti = (List<RapportoDto>)await HttpClient.GetAllRapporti();
            StateHasChanged();
        }

        protected void ViewRapporto(int rapportoId)
        {
            RapportoSelected = ListaRapporti.Single(x => x.Id == rapportoId);
            NavigationManager.NavigateTo($"/detailReport/{rapportoId}");
        }
    }
}
