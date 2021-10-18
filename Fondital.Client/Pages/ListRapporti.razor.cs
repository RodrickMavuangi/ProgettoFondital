using Fondital.Shared.Dto;
using Fondital.Shared.Enums;
using Fondital.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor;

namespace Fondital.Client.Pages
{
    public partial class ListRapporti
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        private List<RapportoDto> ListaRapporti { get; set; }
        private List<string> ListRagioneSociale { get; set; } = new();
        private static List<string> ListStati { get; set; } = new();
        private int PageSize { get; set; }
        private string SearchBySp { get; set; } //affinché il default text si visualizzi il bind-Value deve essere non inizializzato
        private string SearchByStato { get; set; } //affinché il default text si visualizzi il bind-Value deve essere non inizializzato
        private DateTime SearchByDataDa { get; set; } = DateTime.ParseExact("20211001", "yyyyMMdd", null);
        private DateTime SearchByDataA { get; set; } = DateTime.Now;
        private string SearchByCliente { get; set; } = "";
        private string SearchById { get; set; } = "";
        private string SearchByMatricola { get; set; } = "";
        private string SearchByTelefono { get; set; } = "";
        private string SearchByEmail { get; set; } = "";
        public UtenteDto UtenteCorrente { get; set; }
        private bool IsSubmitting = false;
        private bool ShowAddDialog { get; set; } = false;
        public List<RapportoDto> ListaRapportiFiltered => ListaRapporti
        .Where(x => (SearchBySp == Localizer["AllSP"] || Localizer[x.Utente.ServicePartner.RagioneSociale].ToString().Contains(SearchBySp ?? "", StringComparison.InvariantCultureIgnoreCase))
             && (SearchByStato == Localizer["AllStati"] || Localizer[x.Stato.ToString()].ToString().Contains(SearchByStato ?? "", StringComparison.InvariantCultureIgnoreCase))
             && x.DataRapporto.Date >= SearchByDataDa.Date
             && x.DataRapporto.Date <= SearchByDataA.Date
             && ((x.Cliente.Nome ?? "") + " " + (x.Cliente.Cognome ?? "")).Contains(SearchByCliente, StringComparison.InvariantCultureIgnoreCase)
             && x.Id.ToString().Contains(SearchById, StringComparison.InvariantCultureIgnoreCase)
             && (x.Caldaia.Matricola ?? "").Contains(SearchByMatricola, StringComparison.InvariantCultureIgnoreCase)
             && (x.Cliente.NumTelefono ?? "").Contains(SearchByTelefono, StringComparison.InvariantCultureIgnoreCase)
             && (x.Cliente.Email ?? "").Contains(SearchByEmail, StringComparison.InvariantCultureIgnoreCase)
        ).ToList();

        protected override async Task OnInitializedAsync()
        {
            UtenteCorrente = await StateProvider.GetCurrentUser();
            PageSize = Convert.ToInt32(Config["PageSize"]);
            ListStati = EnumExtensions.GetEnumNames<StatoRapporto>().Select(x => Localizer[x].Value).ToList();

            await RefreshRapporti();
        }

        protected async Task RefreshRapporti()
        {
            ListaRapporti = (List<RapportoDto>)await HttpClient.GetAllRapporti();
            if (UtenteCorrente.ServicePartner != null)
            {
                ListRagioneSociale = new() { UtenteCorrente.ServicePartner.RagioneSociale };
                SearchBySp = ListRagioneSociale.Single();
            }
            else
            {
                ListRagioneSociale = ListaRapporti.Select(x => x.Utente.ServicePartner.RagioneSociale).Distinct().ToList();
            }
            StateHasChanged();
        }

        protected void ViewRapporto(int rapportoId)
        {
            NavigationManager.NavigateTo($"/reportDetail/{rapportoId}");
        }

        protected async Task CambiaStato(RapportoDto rapportoToSave, string statoSelezionato)
        {
            IsSubmitting = true;
            rapportoToSave.Stato = Enum.GetValues(typeof(StatoRapporto)).Cast<StatoRapporto>().Single(x => Localizer[x.ToString()] == statoSelezionato);

            try
            {
                await HttpClient.UpdateRapporto(rapportoToSave.Id, rapportoToSave);
                IsSubmitting = false;
            }
            catch (Exception ex)
            {
                await Dialogs.AlertAsync($"{Localizer["ErroreSalvaRapporto"]}: {ex.Message}", Localizer["Errore"]);
                IsSubmitting = false;
            }
        }
    }
}