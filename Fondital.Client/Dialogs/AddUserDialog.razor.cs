using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddUserDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public ServicePartnerDto SPToUpdate { get; set; }
        protected UtenteDto NuovoUtente { get; set; } = new UtenteDto();

        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaUtente()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                NuovoUtente.Email = NuovoUtente.UserName;
                NuovoUtente.IsAbilitato = false;
                await httpClient.sendMailForNewUser(NuovoUtente,SPToUpdate);
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