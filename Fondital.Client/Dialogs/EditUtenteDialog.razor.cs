using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class EditUtenteDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public UtenteDto UtenteToUpdate { get; set; }
        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaUtente()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                await httpClient.UpdateUtente(UtenteToUpdate);
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