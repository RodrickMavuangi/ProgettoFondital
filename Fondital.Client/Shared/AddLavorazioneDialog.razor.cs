using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class AddLavorazioneDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        protected Lavorazione NuovaLavorazione { get; set; } = new Lavorazione();
        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaLavorazione()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                await httpClient.CreateLavorazione(NuovaLavorazione);
                isSubmitting = false;
                await OnSave.InvokeAsync();
            }
            catch (Exception ex)
            {
                isSubmitting = false;
                ErrorMessage = localizer[ex.Message];
            }
        }
    }
}
