using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Telerik.Documents.Core;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Fixed.Model.Fonts;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Zip;

namespace Fondital.Client.Utils
{
    public class StampaService
    {
        private IJSRuntime _jsRuntime { get; set; }
        private NavigationManager _navManager { get; set; }
        private HttpClient _httpClient { get; set; }

        public StampaService(IJSRuntime jsRuntime, NavigationManager navManager, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _navManager = navManager;
            _httpClient = httpClient;
        }

        public async Task StampaDocumenti(RapportoDto rapporto)
        {
            try
            {
                //REGISTRA FONTS
                List<string> fontList = new() { "Cambria_.ttc", "Cambria_b.ttf", "Arial_.ttf", "Arial_b.ttf", "Micross_.ttf" };
                await ImportFonts(fontList);

                //CREA ZIP
                //using Stream stream = File.Open($"docs_{rapporto.Id}.zip", FileMode.Create);
                MemoryStream stream = new();
                using ZipArchive archive = new(stream, ZipArchiveMode.Create, true, null);

                foreach (var docName in new List<string> { "BUH-IT"/*, "BUH-RU", "IT", "RU" */})
                {
                    //APERTURA DOCUMENTO
                    RadFlowDocument document = await ReadDocument($"{docName}.docx");
                    RadFlowDocumentEditor editor = new(document);

                    //POPOLAMENTO
                    editor.ReplaceText("$SPEmail$", rapporto.MotivoIntervento);

                    //CONVERSIONE IN PDF
                    PdfFormatProvider pdfProvider = new();
                    var docAsPdf = pdfProvider.Export(document);

                    //ADD TO ZIP
                    await archive.CreateEntry($"{docName}.pdf").Open().WriteAsync(docAsPdf);
                    //using ZipArchiveEntry entry = archive.CreateEntry($"{docName}.pdf"); //?
                    //BinaryWriter writer = new(entry.Open());
                    //writer.Write(docAsPdf);
                    //writer.Flush();

                    //DOWNLOAD PDF
                    //var js = (IJSInProcessRuntime)_jsRuntime;
                    //await js.InvokeVoidAsync("saveFile", Convert.ToBase64String(docAsPdf), "application/pdf", $"{docName}.pdf");
                }

                //DOWNLOAD ZIP

                var js = (IJSInProcessRuntime)_jsRuntime;
                await js.InvokeVoidAsync("saveFile", Convert.ToBase64String(stream.ToArray()), "application/zip", $"docs_{rapporto.Id}.zip");
            }
            catch (Exception ex)
            {
                throw;
            }
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