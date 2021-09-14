using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class AddSPDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }

        protected ServicePartner NuovoSP { get; set; } = new ServicePartner();

        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaSP()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                await httpClient.CreateServicePartner(NuovoSP);
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