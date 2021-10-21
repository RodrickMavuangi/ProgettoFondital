using Fondital.Client.Utils;
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
    public partial class DetailRapporto
    {
        [CascadingParameter] DialogFactory Dialogs { get; set; }
        [Parameter] public string Id { get; set; }
        private List<RapportoVoceCostoDto> RapportiVociCosto { get; set; } = new();
        private RicambioDto NewRicambio { get; set; } = new();
        public List<LavorazioneDto> ListaLavorazioni { get; set; } = new();
        public List<string> LavorazioniDescription { get; set; } = new();
        public RapportoVoceCostoDto RapportoVoceCostoSelected { get; set; } = new();
        public string Modello { get; set; }
        protected List<string> CampiDaCompilare { get; set; } = new();
        private int CurrentStepIndex { get; set; }
        private string CurrentCulture { get; set; }
        private bool AbilitaModifica { get; set; } = true;
        private bool ShowEditVoceCosto { get; set; } = false;
        private bool ShowAddRicambio { get; set; } = false;
        private bool ShowAddVoceCosto { get; set; } = false;
        private bool ShowCampiObbligatori { get; set; } = false;
        private bool IsPrinting { get; set; } = false;
        private bool IsEdited { get; set; } = false;
        private RapportoDto Rapporto { get; set; } = new();
        public UtenteDto UtenteCorrente { get; set; }
        private static IEnumerable<string> ListStati { get => EnumExtensions.GetEnumNames<StatoRapporto>(); }

        protected override async Task OnInitializedAsync()
        {
            UtenteCorrente = await StateProvider.GetCurrentUser();
            CurrentCulture = await StateProvider.GetCurrentCulture();

            try
            {
                if (string.IsNullOrEmpty(Id))
                    Rapporto.Utente = UtenteCorrente;
                else
                    Rapporto = await RapportoClient.GetRapportoById(int.Parse(Id)); //c'è il parse così se viene inserito un url malformato lancia errore

                //se sei un service partner e il rapporto non è aperto o rifiutato
                if (UtenteCorrente.ServicePartner != null && !(Rapporto.Stato == StatoRapporto.Aperto || Rapporto.Stato == StatoRapporto.Rifiutato))
                    AbilitaModifica = false;

                ListaLavorazioni = (List<LavorazioneDto>)await LavorazioneClient.GetAllLavorazioni(true);

                if (CurrentCulture == "it-IT")
                    LavorazioniDescription = ListaLavorazioni.Select(x => x.NomeItaliano).ToList();
                else
                    LavorazioniDescription = ListaLavorazioni.Select(x => x.NomeRusso).ToList();
            }
            catch
            {
                NavigationManager.NavigateTo("/reports");
            }
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddVoceCosto = false;
            ShowEditVoceCosto = false;
            ShowAddRicambio = false;
            await InvokeAsync(StateHasChanged);
        }

        protected async Task RemoveVoceCosto(RapportoVoceCostoDto rapportoVoceCosto)
        {
            RapportiVociCosto.Remove(rapportoVoceCosto);
            IsEdited = true;
            await CloseAndRefresh();
        }

        protected async Task AggiungiRicambio()
        {
            Rapporto.Ricambi.Add(NewRicambio);
            IsEdited = true;
            await CloseAndRefresh();
        }

        protected void EditVoceCosto(RapportoVoceCostoDto rapportoVoceCosto)
        {
            RapportoVoceCostoSelected = rapportoVoceCosto;
            ShowEditVoceCosto = true;
        }

        protected async Task CambiaStato(string statoSelezionato)
        {
            IsEdited = true;
            await Salva(Enum.GetValues(typeof(StatoRapporto)).Cast<StatoRapporto>().Single(x => Localizer[x.ToString()] == statoSelezionato));
        }

        protected async Task CambiaStep(int newStep)
        {
            if (await Salva())
                CurrentStepIndex = newStep;
        }

        //protected async void GetModelloCaldaia() =>
        //    Modello = await RestClient.ModelloCaldaiaService(Rapporto.Caldaia.Matricola ?? "");

        protected async Task Stampa()
        {
            IsPrinting = true;

            try
            {
                await StampaService.StampaDocumento(Rapporto);
            }
            catch
            {
                throw;
            }

            IsPrinting = false;
        }

        protected async Task<bool> Salva(StatoRapporto? newStatus = null)
        {
            if (IsEdited)
            {
                try
                {
                    IsEdited = false;

                    if (Rapporto.Id == 0)
                    {   //il rapporto va creato
                        Rapporto.Id = await RapportoClient.CreateRapporto(Rapporto);
                    }
                    else if (newStatus == null)
                    {   //il rapporto va aggiornato
                        if (Rapporto.Stato == StatoRapporto.Aperto || Rapporto.Stato == StatoRapporto.Rifiutato)
                        {
                            await RapportoClient.UpdateRapporto(Rapporto.Id, Rapporto);
                        }
                        else
                        {   //aggiornamento della direzione: mantenere la consistenza del rapporto
                            CampiDaCompilare = CheckCampiObbligatori();
                            if (CampiDaCompilare.Count == 0)
                                await RapportoClient.UpdateRapporto(Rapporto.Id, Rapporto);
                            else
                            {
                                ShowCampiObbligatori = true;
                                IsEdited = true;
                                return false;
                            }
                        }
                    }
                    else if (newStatus != null)
                    {   //oltre all'aggiornamento va cambiato stato
                        CampiDaCompilare = CheckCampiObbligatori();
                        if (CampiDaCompilare.Count == 0)
                        {
                            Rapporto.Stato = newStatus.Value;
                            await RapportoClient.UpdateRapporto(Rapporto.Id, Rapporto);
                            //NavigationManager.NavigateTo("/reports"); //non mi piace
                        }
                        else
                        {
                            ShowCampiObbligatori = true;
                            IsEdited = true;
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Dialogs.AlertAsync($"{Localizer["ErroreSalvaRapporto"]}: {ex.Message}", Localizer["Errore"]);
                    return false;
                }
            }

            return true;
        }

        protected List<string> CheckCampiObbligatori()
        {
            List<string> CampiDaCompilare = new();

            if (string.IsNullOrEmpty(Rapporto.Cliente.Nome)) CampiDaCompilare.Add(Localizer["NomeCliente"]);
            if (string.IsNullOrEmpty(Rapporto.Cliente.Cognome)) CampiDaCompilare.Add(Localizer["CognomeCliente"]);
            if (string.IsNullOrEmpty(Rapporto.Cliente.Citta)) CampiDaCompilare.Add(Localizer["CittaCliente"]);
            if (string.IsNullOrEmpty(Rapporto.Cliente.Via)) CampiDaCompilare.Add(Localizer["ViaCliente"]);
            if (string.IsNullOrEmpty(Rapporto.Cliente.NumCivico)) CampiDaCompilare.Add(Localizer["CivicoCliente"]);
            if (string.IsNullOrEmpty(Rapporto.Cliente.NumTelefono)) CampiDaCompilare.Add(Localizer["TelefonoCliente"]);
            if (string.IsNullOrEmpty(Rapporto.Cliente.Email)) CampiDaCompilare.Add(Localizer["EmailCliente"]);
            if (string.IsNullOrEmpty(Rapporto.Caldaia.Matricola)) CampiDaCompilare.Add(Localizer["MatricolaCaldaia"]);
            //if (string.IsNullOrEmpty(Rapporto.Caldaia.Modello)) CampiDaCompilare.Add(Localizer["ModelloCaldaia"]);            commentato per effettuare test
            //if (string.IsNullOrEmpty(Rapporto.Caldaia.Versione)) CampiDaCompilare.Add(Localizer["VersioneCaldaia"]);          commentato per effettuare test
            //if (Rapporto.Caldaia.DataVendita == null) CampiDaCompilare.Add(Localizer["DataVenditaCaldaia"]);                  commentato per effettuare test
            //if (Rapporto.Caldaia.DataMontaggio == null) CampiDaCompilare.Add(Localizer["DataMontaggioCaldaia"]);              commentato per effettuare test
            //if (Rapporto.Caldaia.DataAvvio == null) CampiDaCompilare.Add(Localizer["DataAvvioCaldaia"]);                      commentato per effettuare test
            //if (string.IsNullOrEmpty(Rapporto.Caldaia.TecnicoPrimoAvvio)) CampiDaCompilare.Add(Localizer["TecnicoCaldaia"]);  commentato per effettuare test
            //if (Rapporto.Caldaia.NumCertificatoTecnico == null) CampiDaCompilare.Add(Localizer["NumCertificatoCaldaia"]); //non obbligatorio
            //if (string.IsNullOrEmpty(Rapporto.Caldaia.DittaPrimoAvvio)) CampiDaCompilare.Add(Localizer["DittaAvvioCaldaia"]); commentato per effettuare test
            if (Rapporto.DataIntervento == null) CampiDaCompilare.Add(Localizer["RapportoDataIntervento"]);
            if (string.IsNullOrEmpty(Rapporto.MotivoIntervento)) CampiDaCompilare.Add(Localizer["RapportoMotivoIntervento"]);
            if (string.IsNullOrEmpty(Rapporto.TipoLavoro)) CampiDaCompilare.Add(Localizer["RapportoTipologiaLavoro"]);
            if (string.IsNullOrEmpty(Rapporto.NomeTecnico)) CampiDaCompilare.Add(Localizer["RapportoNomeTecnico"]);
            //if (Rapporto.Ricambi.Count == 0) CampiDaCompilare.Add(Localizer["RapportoRicambi"]);                              commentato per effettuare test
            //if (Rapporto.RapportiVociCosto.Count == 0) CampiDaCompilare.Add(Localizer["RapportoVociCosto"]);                  commentato per effettuare test

            return CampiDaCompilare;
        }
    }
}