using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddDifettoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        protected Difetto NuovoDifetto { get; set; } = new Difetto();
        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaDifetto()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                await httpClient.CreateDifetto(NuovoDifetto);
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
