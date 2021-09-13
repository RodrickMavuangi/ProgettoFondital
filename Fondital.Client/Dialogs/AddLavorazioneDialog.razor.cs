using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddLavorazioneDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        protected Lavorazione NuovaLavorazione { get; set; }
        protected bool isSubmitting { get; set; }

        protected async override Task OnInitializedAsync()
        {
            NuovaLavorazione = new Lavorazione()
            {
                NomeItaliano = "",
                NomeRusso = "",
                IsAbilitato = true
            };
        }

        protected async Task SalvaLavorazione()
        {
            isSubmitting = true;

            try
            {
                await httpClient.CreateLavorazione(NuovaLavorazione);
                isSubmitting = false;
                await OnSave.InvokeAsync();
            }
            catch
            {
                isSubmitting = false;
                throw;
            }
        }
    }
}
