using Fondital.Shared.Dto;
using Fondital.Shared.Enums;
using Fondital.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class ListRapporti
    {
        private List<RapportoDto> ListaRapporti { get; set; }
        private List<string> ListRagioneSociale { get; set; } = new();
        private static IEnumerable<string> ListStati { get => EnumExtensions.GetEnumNames<StatoRapporto>(); }
        private int PageSize { get; set; } = 10;
        private string SearchBySp { get; set; } = "";
        private string SearchByStato { get; set; } = "";
        private DateTime? SearchByDataFirst { get; set; }
        private DateTime? SearchByDataLast { get; set; }
        private string SearchByCliente { get; set; } = "";
        private string SearchById { get; set; }
        private string SearchByMatricola { get; set; } = "";
        private string SearchByTelefono { get; set; } = "";
        private string SearchByEmail { get; set; } = "";
        private bool LoadingVar { get; set; }
        protected bool ShowAddDialog { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            LoadingVar = true;
            PageSize = Convert.ToInt32(Config["PageSize"]);
            await RefreshRapporti();
        }

        public List<RapportoDto> ListaRapportiFiltered => ListaRapporti
            .Where(x => x.Utente.ServicePartner.RagioneSociale.Contains(SearchBySp, StringComparison.InvariantCultureIgnoreCase)
                     && x.Stato.ToString().Contains(SearchByStato, StringComparison.InvariantCultureIgnoreCase)
                     && x.DataRapporto >= SearchByDataFirst
                     && x.DataRapporto <= SearchByDataLast
                     && (x.Cliente.Nome + " " + x.Cliente.Cognome).Contains(SearchByCliente, StringComparison.InvariantCultureIgnoreCase)
                     //&& x.Id.ToString().StartsWith(SearchById)!!!
                     //&& x.Caldaia.Matricola.Contains(SearchByMatricola, StringComparison.InvariantCultureIgnoreCase)!!!
                     && x.Cliente.NumTelefono.ToString().Contains(SearchByTelefono, StringComparison.InvariantCultureIgnoreCase)
                     && x.Cliente.Email.Contains(SearchByEmail, StringComparison.InvariantCultureIgnoreCase)
            ).ToList();

        protected async Task RefreshRapporti()
        {
            ListaRapporti = (List<RapportoDto>)await HttpClient.GetAllRapporti();
            ListRagioneSociale = ListaRapporti.Select(x => x.Utente.ServicePartner.RagioneSociale).Distinct().ToList();
            LoadingVar = false;
            StateHasChanged();
        }

        protected void ViewRapporto(int rapportoId)
        {
            NavigationManager.NavigateTo($"/reportDetail/{rapportoId}");
        }
    }
}
