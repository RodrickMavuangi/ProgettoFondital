using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class EditVoceCostoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public VoceCosto VoceCostoToUpdate { get; set; }
        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaVoceCosto()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                await httpClient.UpdateVoceCosto(VoceCostoToUpdate.Id, VoceCostoToUpdate);
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
