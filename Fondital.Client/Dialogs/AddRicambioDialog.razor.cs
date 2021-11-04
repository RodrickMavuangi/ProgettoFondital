using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddRicambioDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [Parameter] public EventCallback<RicambioDto> RicambioChanged { get; set; }
        [Parameter] public RicambioDto NewRicambio { get; set; } = new();
        protected RicambioRequestDto RicambioRequest { get; set; } = new();
        private bool IsSubmitting = false;
        private string ErrorMessage = "";

        public async Task Done()
        {
            IsSubmitting = true;
            try
            {
                NewRicambio = await RestClient.GetPezzoRicambio(RicambioRequest);
                await RicambioChanged.InvokeAsync(NewRicambio);
                await OnSave.InvokeAsync();
            }
            catch (Exception ex)
            {
                IsSubmitting = false;
                ErrorMessage = localizer[ex.Message]; throw;
            }
        }
    }
}