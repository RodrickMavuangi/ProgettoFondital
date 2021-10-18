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
using Telerik.Windows.Documents.Common.FormatProviders;
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

        public StampaService(IJSRuntime jsRuntime, NavigationManager navManager)
        {
            _jsRuntime = jsRuntime;
            _navManager = navManager;
        }

        public async Task StampaDocumento(RapportoDto rapporto)
        {
            try
            {
                //APERTURA DOCUMENTO
                var url = Path.Combine(_navManager.BaseUri, "DocumentTemplates", "BUH-IT.docx");

                RadFlowDocument document = await ReadFile(url);
                RadFlowDocumentEditor editor = new(document);

                //POPOLAMENTO
                editor.ReplaceText("$SPEmail$", rapporto.MotivoIntervento);

                //ESPORTAZIONE IN PDF
                PdfFormatProvider pdfProvider = new();
                var docAsPdf = pdfProvider.Export(document);

                //DOWNLOAD PDF
                var js = (IJSInProcessRuntime)_jsRuntime;
                //await js.InvokeVoidAsync("saveFile", Encoding.UTF8.GetString(docAsPdf), "application/pdf", "BUH-IT.pdf");
                await js.InvokeVoidAsync("saveFile", Convert.ToBase64String(docAsPdf), "application/pdf", "BUH-IT.pdf");

                //TODO: CREAZIONE E DOWNLOAD ZIP
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<RadFlowDocument> ReadFile(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            IFormatProvider<RadFlowDocument> fileFormatProvider = new DocxFormatProvider();
            Stream stream = response.Content.ReadAsStream();
            return fileFormatProvider.Import(stream);
        }
    }
}