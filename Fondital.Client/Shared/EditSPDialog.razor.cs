using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class EditSPDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public ServicePartner SPToUpdate { get; set; }

        protected bool isSubmitting = false;

        protected string ErrorMessage = "";

        protected async Task SalvaSp()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                await servicePartnerClient.UpdateServicePartner(SPToUpdate.Id, SPToUpdate);
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