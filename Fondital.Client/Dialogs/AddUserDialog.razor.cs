using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Telerik.Blazor;

namespace Fondital.Client.Dialogs
{
    public partial class AddUserDialog
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
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
                NuovoUtente.ServicePartner = SPToUpdate;
                await httpClient.sendMailForNewUser(NuovoUtente);
                isSubmitting = false;
                await OnSave.InvokeAsync();
                await Dialogs.ConfirmAsync($"{@localizer["MailInviata"]} {NuovoUtente.Nome} {NuovoUtente.Cognome} {localizer["SettaPassword"]}");
            }
            catch (Exception ex)
            {
                isSubmitting = false;
                ErrorMessage = localizer[ex.Message];
            }
        }
    }
}