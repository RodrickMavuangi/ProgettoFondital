using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class AddDifettoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        protected Difetto NuovoDifetto { get; set; }
        protected bool isSubmitting = false;

        protected async override Task OnInitializedAsync()
        {
            NuovoDifetto = new Difetto() { NomeItaliano = "", NomeRusso = "", IsAbilitato = true };
        }

        protected async Task SalvaDifetto()
        {
            isSubmitting = true;

            try
            {
                await httpClient.CreateDifetto(NuovoDifetto);
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
