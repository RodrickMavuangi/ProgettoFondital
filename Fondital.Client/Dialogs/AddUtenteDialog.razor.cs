using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Telerik.Blazor;

namespace Fondital.Client.Dialogs
{
    public partial class AddUtenteDialog
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public ServicePartnerDto SP { get; set; }
        public UtenteDto NuovoUtente { get; set; } = new();
        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaUtente()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                NuovoUtente.Email = NuovoUtente.UserName;
                NuovoUtente.ServicePartner = SP;
                await httpClient.sendMailForNewUser(NuovoUtente);
                isSubmitting = false;
                await OnSave.InvokeAsync();
                await Dialogs.ConfirmAsync($"{@localizer["MailInviata"]} {NuovoUtente.Nome} {NuovoUtente.Cognome}.", " ");
            }
            catch (Exception ex)
            {
                isSubmitting = false;
                ErrorMessage = localizer[ex.Message];
            }
        }
    }
}