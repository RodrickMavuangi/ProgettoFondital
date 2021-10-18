using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
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
                List<string> fontList = new() { "Cambria" };
                await ImportFonts(fontList);

                //APERTURA DOCUMENTO
                RadFlowDocument document = await ReadDocument("BUH-IT.docx");
                RadFlowDocumentEditor editor = new(document);

                //POPOLAMENTO
                editor.ReplaceText("$SPEmail$", rapporto.MotivoIntervento);

                //ESPORTAZIONE IN PDF
                PdfFormatProvider pdfProvider = new();
                var docAsPdf = pdfProvider.Export(document);
                
                //DOWNLOAD PDF
                var js = (IJSInProcessRuntime)_jsRuntime;
                await js.InvokeVoidAsync("saveFile", Convert.ToBase64String(docAsPdf), "application/pdf", "BUH-IT.pdf");

                //TODO: DOWNLOAD ZIP
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
                var response = await _httpClient.GetAsync(Path.Combine(_navManager.BaseUri, "Documents/Fonts", $"{font}.TTC"));

                using (MemoryStream ms = new MemoryStream())
                {
                    response.Content.ReadAsStream().CopyTo(ms);
                    FontsRepository.RegisterFont(new FontFamily(font), FontStyles.Normal, FontWeights.Normal, ms.ToArray());
                }
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