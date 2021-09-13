using Fondital.Shared.Enums;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddVoceCostoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        protected VoceCosto NuovaVoceCosto { get; set; } = new VoceCosto();
        protected bool isSubmitting = false;

        protected async Task SalvaVoceCosto()
        {
            isSubmitting = true;

            try
            {
                await httpClient.CreateVoceCosto(NuovaVoceCosto);
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
