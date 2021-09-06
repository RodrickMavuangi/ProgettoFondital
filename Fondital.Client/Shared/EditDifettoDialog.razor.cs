using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class EditDifettoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public Difetto DifettoToUpdate { get; set; }
        protected bool isSubmitting = false;

        protected async Task SalvaDifetto()
        {
            isSubmitting = true;

            try
            {
                await httpClient.UpdateDifetto(DifettoToUpdate.Id, DifettoToUpdate);
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
