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
        protected ServicePartnerRequestDto servicePartnerRequestDto { get; set; } = new();

        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaSP()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                servicePartnerRequestDto = new ServicePartnerRequestDto() { CodiceFornitore = NuovoSP.CodiceFornitore };
                NuovoSP = await ExternalServiceClient.GetDettagliSP(servicePartnerRequestDto);
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