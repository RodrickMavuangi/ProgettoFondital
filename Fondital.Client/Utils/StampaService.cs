using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;

namespace Fondital.Client.Utils
{
    public class StampaService
    {
        private IJSRuntime _jsRuntime { get; set; }
        private NavigationManager _navManager { get; set; }
        private HttpClient _httpClient { get; set; }
        private IConfiguration _config { get; set; }
        private RapportoDto Rapporto { get; set; }
        private RadFlowDocumentEditor Editor { get; set; }
        private int templateTableRows { get; set; }

        public StampaService(IJSRuntime jsRuntime, NavigationManager navManager, HttpClient httpClient, IConfiguration config)
        {
            _jsRuntime = jsRuntime;
            _navManager = navManager;
            _httpClient = httpClient;
            _config = config;
        }

        public async Task StampaDocumenti(RapportoDto rapporto)
        {
            Rapporto = rapporto;
            templateTableRows = Convert.ToInt32(_config["TemplateTableRows"]);

            try
            {
                //REGISTRA FONTS
                List<string> fontList = new() { "Arial_.ttf", "Arial_b.ttf", "Arial_b_i.ttf", "Calibri.ttf", "Cambria_.ttc", "Cambria_b.ttf", "Micross_.ttf", "Micross_i.ttf" };
                await ImportFonts(fontList);

                //CREAZIONE ZIP
                using MemoryStream zipStream = new();
                using ZipArchive archive = new(zipStream, ZipArchiveMode.Create, true);

                foreach (var docName in new List<string> { "BUH-IT", "BUH-RU", "AKT-IT", "AKT-RU" })
                {
                    //APERTURA DOCUMENTO
                    RadFlowDocument document = await ReadDocument($"{docName}.docx");
                    Editor = new(document);

                    //POPOLAMENTO
                    PopolaCampi(docName);

                    //CONVERSIONE IN PDF E AGGIUNTA ALLO ZIP
                    PdfFormatProvider pdfProvider = new();
                    using Stream pdfStream = archive.CreateEntry($"{docName}.pdf", CompressionLevel.Optimal).Open();
                    pdfProvider.Export(document, pdfStream);
                }

                //CHIUSURA ZIP E DOWNLOAD
                archive.Dispose();
                var js = (IJSInProcessRuntime)_jsRuntime;
                await js.InvokeVoidAsync("saveFile", Convert.ToBase64String(zipStream.ToArray()), "application/zip", $"docs_{rapporto.Id}.zip");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void PopolaCampi(string docName)
        {
            switch (docName)
            {
                case "BUH-IT":
                case "BUH-RU":
                    TrimRigheTabella(0, Rapporto.RapportiVociCosto.Count);
                    TrimRigheTabella(1, Rapporto.Ricambi.Count);

                    #region SERVICE PARTNER
                    //ci vuole?
                    #endregion

                    #region CLIENTE
                    Editor.ReplaceText("$ClienteRagioneSociale$", ""); //TODO
                    Editor.ReplaceText("$ClienteIndirizzo$", "");       //TODO
                    Editor.ReplaceText("$ClienteCF$", "");       //TODO
                    Editor.ReplaceText("$ClienteCausaleRegistraz$", "");       //TODO
                    Editor.ReplaceText("$ClienteCC$", "");       //TODO
                    Editor.ReplaceText("$ClienteBanca$", "");       //TODO
                    Editor.ReplaceText("$ClienteContoCorrisp$", "");       //TODO
                    Editor.ReplaceText("$ClienteCodBanca$", "");       //TODO
                    Editor.ReplaceText("$ClienteTelefono$", "");       //TODO
                    #endregion

                    #region CALDAIA
                    Editor.ReplaceText("$Lavorazione$", Rapporto.TipoLavoro);
                    Editor.ReplaceText("$Matricola$", Rapporto.Caldaia.Matricola);
                    Editor.ReplaceText("$Indirizzo$", $"{Rapporto.Cliente.Via} {Rapporto.Cliente.NumCivico}, {Rapporto.Cliente.Citta}");
                    Editor.ReplaceText("$Tecnico$", Rapporto.NomeTecnico);
                    #endregion

                    #region VOCI
                    int costoTotVoci = 0;
                    int costoVoce = 0;
                    foreach (var (voce, i) in Rapporto.RapportiVociCosto.Select((value, index) => (value, index)))
                    {
                        Editor.ReplaceText($"$VoceDescr{i}$", docName == "BUH-IT" ? voce.VoceCosto.NomeItaliano : voce.VoceCosto.NomeRusso);
                        Editor.ReplaceText($"$VoceData{i}$", Rapporto.DataIntervento?.ToShortDateString());
                        Editor.ReplaceText($"$VoceQuantita{i}$", voce.Quantita.ToString());
                        costoVoce = (voce.Quantita * voce.VoceCosto.Listini.FirstOrDefault(x => x.ServicePartner == Rapporto.Utente.ServicePartner).Valore);
                        costoTotVoci += costoVoce;
                        Editor.ReplaceText($"$VoceCosto{i}$", $"₽ {costoVoce}");
                    }
                    Editor.ReplaceText("$VociNumTot$", Rapporto.RapportiVociCosto.Sum(x => x.Quantita).ToString());
                    Editor.ReplaceText("$VociCostoTot$", $"₽ {costoTotVoci}");
                    #endregion

                    #region RICAMBI
                    foreach (var (ricambio, i) in Rapporto.Ricambi.Select((value, index) => (value, index)))
                    {
                        Editor.ReplaceText($"$RicambioCode{i}$", "");           //TODO introdurre codice ricambio
                        Editor.ReplaceText($"$RicambioDescr{i}$", ricambio.Descrizione);
                        Editor.ReplaceText($"$RicambioCosto{i}$", $"₽ {ricambio.Costo}");
                        Editor.ReplaceText($"$RicambioQta{i}$", ricambio.Quantita.ToString());
                        Editor.ReplaceText($"$RicambioTot{i}$", $"₽ {ricambio.Quantita * ricambio.Costo}");
                    }
                    Editor.ReplaceText("$RicambiNumTot$", Rapporto.Ricambi.Sum(x => x.Quantita).ToString());
                    int costoTotRicambi = Rapporto.Ricambi.Sum(x => x.Quantita * x.Costo);
                    Editor.ReplaceText("$RicambiCostoTot$", $"₽ {costoTotRicambi}");
                    #endregion

                    #region FONDO PAGINA
                    Editor.ReplaceText("$Totale$", $"₽ {costoTotVoci + costoTotRicambi}");
                    Editor.ReplaceText("$NomeDitta$", "");                      //TODO
                    Editor.ReplaceText("$NomeDirettore$", "");                  //TODO
                    #endregion

                    break;
                case "AKT-IT":
                case "AKT-RU":
                    TrimRigheTabella(1, Rapporto.Ricambi.Count);

                    #region CALDAIA
                    Editor.ReplaceText("$MatricolaCaldaia$", Rapporto.Caldaia.Matricola);
                    Editor.ReplaceText("$TipoCaldaia$", "installata a muro"); //TODO
                    Editor.ReplaceText("$DataVendita$", Rapporto.Caldaia.DataVendita?.ToShortDateString());
                    Editor.ReplaceText("$MarcaCaldaia$", ""); //TODO
                    Editor.ReplaceText("$Venditore$", ""); //TODO
                    Editor.ReplaceText("$ModelloCaldaia$", Rapporto.Caldaia.Model);
                    Editor.ReplaceText("$DataInstallazione$", Rapporto.Caldaia.DataMontaggio?.ToShortDateString());
                    Editor.ReplaceText("$DataPrimaAccens$", Rapporto.Caldaia.DataAvvio?.ToShortDateString());
                    Editor.ReplaceText("$Produttore$", ""); //TODO
                    Editor.ReplaceText("$TecnicoPrimaAccensione$", Rapporto.Caldaia.TecnicoPrimoAvvio);
                    Editor.ReplaceText("$NumCertificatoTecnico$", Rapporto.Caldaia.NumCertificatoTecnico.ToString());
                    Editor.ReplaceText("$DittaPrimaAccensione$", Rapporto.Caldaia.DittaPrimoAvvio);
                    #endregion

                    #region CLIENTE
                    Editor.ReplaceText("$CittaUtente$", Rapporto.Cliente.Citta);
                    Editor.ReplaceText("$ViaUtente$", $"{Rapporto.Cliente.Via}, {Rapporto.Cliente.NumCivico}");
                    Editor.ReplaceText("$TelefonoUtente$", Rapporto.Cliente.NumTelefono);
                    Editor.ReplaceText("$NomeUtente$", Rapporto.Cliente.FullName);
                    #endregion

                    #region INTERVENTO
                    Editor.ReplaceText("$DataIntervento$", Rapporto.DataIntervento?.ToShortDateString());
                    Editor.ReplaceText("$TecnicoIntervento$", Rapporto.NomeTecnico);
                    Editor.ReplaceText("$MotivoRiparazione$", Rapporto.MotivoIntervento);
                    Editor.ReplaceText("$LavoroEffettuato$", Rapporto.TipoLavoro);
                    #endregion

                    #region RICAMBI
                    foreach (var (ricambio, i) in Rapporto.Ricambi.Select((value, index) => (value, index)))
                    {
                        Editor.ReplaceText($"$RicambioCode{i}$", "");           //TODO introdurre codice ricambio
                        Editor.ReplaceText($"$RicambioDescr{i}$", ricambio.Descrizione);
                        Editor.ReplaceText($"$RicambioQta{i}$", ricambio.Quantita.ToString());
                    }
                    #endregion

                    #region COSTI
                    int costoRicambi = Rapporto.Ricambi.Sum(x => x.Quantita * x.Costo);
                    int costoIntervento = 0;
                    int costoTrasporto = 0;
                    Editor.ReplaceText("$CostoRicambi$", costoRicambi.ToString());
                    Editor.ReplaceText("$CostoIntervento$", ""); //TODO
                    Editor.ReplaceText("$CostoTrasporto$", ""); //TODO
                    Editor.ReplaceText("$CostoTotale$", (costoRicambi + costoIntervento + costoTrasporto).ToString());
                    #endregion

                    break;
            }
        }

        private void TrimRigheTabella(int tableIndex, int righeDaTenere)
        {
            //seleziona la tabella numero tableIndex e elimina le righe in eccesso per tenere solo quelle che servono
            Table table = Editor.Document.EnumerateChildrenOfType<Table>().ToList()[tableIndex];
            table.Rows.RemoveRange(table.Rows.Count - templateTableRows - 1 + righeDaTenere, templateTableRows - righeDaTenere);
        }

        private async Task ImportFonts(List<string> FileNames)
        {
            foreach (var font in FileNames)
            {
                var response = await _httpClient.GetAsync(Path.Combine(_navManager.BaseUri, "Documents/Fonts", $"{font}"));
                FontsRepository.RegisterFont(
                    new FontFamily(font.Substring(0, font.IndexOf('_'))),
                    font.Contains("_i") ? FontStyles.Italic : FontStyles.Normal,
                    font.Contains("_b") ? FontWeights.Bold : FontWeights.Normal,
                    response.Content.ReadAsByteArrayAsync().Result);
            }
        }

        private async Task<RadFlowDocument> ReadDocument(string docName)
        {
            var response = await _httpClient.GetAsync(Path.Combine(_navManager.BaseUri, "Documents/Templates", docName));

            IFormatProvider<RadFlowDocument> fileFormatProvider = new DocxFormatProvider();
            Stream stream = response.Content.ReadAsStream();
            return fileFormatProvider.Import(stream);
        }
    }
}