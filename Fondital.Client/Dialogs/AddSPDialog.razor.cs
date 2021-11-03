using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddSPDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }

        protected ServicePartnerDto NuovoSP { get; set; } = new ServicePartnerDto();

        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaSP()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                NuovoSP = await ExternalServiceClient.GetDettagliSP(NuovoSP);
                await spClient.CreateServicePartner(NuovoSP);
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