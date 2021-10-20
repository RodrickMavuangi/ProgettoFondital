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
                List<string> fontList = new() { "Cambria_.ttc", "Cambria_b.ttf", "Arial_.ttf", "Arial_b.ttf", "Micross_.ttf" };
                await ImportFonts(fontList);

                //CREAZIONE ZIP
                using MemoryStream stream = new();
                using ZipArchive archive = new(stream, ZipArchiveMode.Create, true);

                foreach (var docName in new List<string> { "BUH-IT"/*, "BUH-RU", "AKT-IT", "AKT-RU" */})
                {
                    //APERTURA DOCUMENTO
                    RadFlowDocument document = await ReadDocument($"{docName}.docx");
                    Editor = new(document);

                    //POPOLAMENTO
                    PopolaCampi(docName);

                    //CONVERSIONE IN PDF E AGGIUNTA ALLO ZIP
                    PdfFormatProvider pdfProvider = new();
                    pdfProvider.Export(document, archive.CreateEntry($"{docName}.pdf", CompressionLevel.Optimal).Open());
                }

                //CHIUSURA ZIP E DOWNLOAD
                archive.Dispose();
                var js = (IJSInProcessRuntime)_jsRuntime;
                await js.InvokeVoidAsync("saveFile", Convert.ToBase64String(stream.ToArray()), "application/zip", $"docs_{rapporto.Id}.zip");
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
                    TrimRigheTabella(0, 4);
                    TrimRigheTabella(1, 3);
                    //editor.ReplaceText("$SPEmail$", Rapporto.Utente.Email);
                    break;
                case "AKT-IT":
                case "AKT-RU":
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
                FontsRepository.RegisterFont(new FontFamily(font.Substring(0, font.IndexOf('_'))), FontStyles.Normal, font.Contains("_b") ? FontWeights.Bold : FontWeights.Normal, response.Content.ReadAsByteArrayAsync().Result);
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