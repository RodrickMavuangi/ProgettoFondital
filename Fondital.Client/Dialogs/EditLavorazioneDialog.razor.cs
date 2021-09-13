using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class EditLavorazioneDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public Lavorazione LavorazioneToUpdate { get; set; }
        protected bool isSubmitting = false;

        protected async Task SalvaLavorazione()
        {
            isSubmitting = true;

            try
            {
                await httpClient.UpdateLavorazione(LavorazioneToUpdate.Id, LavorazioneToUpdate);
                isSubmitting = false;
                await OnSave.InvokeAsync();
            }
            catch (Exception ex)
            {
                isSubmitting = false;
                throw;
            }
        }
    }
}