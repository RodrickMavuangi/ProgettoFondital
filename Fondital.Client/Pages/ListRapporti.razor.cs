using Fondital.Shared.Dto;
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
        private DateTime SearchByDate { get; set; }
        private string SearchByCliente { get; set; } = "";
        private string SearchById { get; set; }
        private string SearchByMatricola { get; set; } = "";
        private string SearchByTelefono { get; set; } = "";
        private string SearchByEmail { get; set; } = "";

        // Temporary for Data search input
        private string SearchText { get; set; } = "";

        protected bool ShowAddDialog { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            PageSize = Convert.ToInt32(Config["PageSize"]);
            await RefreshRapporti();
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddDialog = false;
            await RefreshRapporti();
        }

        // Missing date filter
        public List<RapportoDto> ListaRapporti_filtered => ListaRapporti
            .Where(x => x.Utente.ServicePartner.RagioneSociale.Contains(SearchBySp, StringComparison.InvariantCultureIgnoreCase)
                     && x.Stato.ToString().Contains(SearchByStato, StringComparison.InvariantCultureIgnoreCase)
                     && x.DataRapporto.Equals(SearchByDate)
                     && (x.Cliente.Nome + " " + x.Cliente.Cognome).Contains(SearchByCliente, StringComparison.InvariantCultureIgnoreCase)
                     && x.Id.ToString().StartsWith(SearchById)
                     && x.Caldaia.Matricola.Contains(SearchByMatricola, StringComparison.InvariantCultureIgnoreCase)
                     && x.Cliente.NumTelefono.ToString().Contains(SearchByTelefono, StringComparison.InvariantCultureIgnoreCase)
                     && x.Cliente.Email.Contains(SearchByEmail, StringComparison.InvariantCultureIgnoreCase))
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
