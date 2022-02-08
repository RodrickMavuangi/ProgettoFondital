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
        [Parameter] public RicambioDto Ricambio { get; set; } = new();
        protected RicambioRequestDto RicambioRequest { get; set; } = new();
        private bool IsSubmitting = false;
        private string ErrorMessage = "";

        public async Task Done()
        {
            IsSubmitting = true;
            try
            {
                Ricambio = await RestClient.GetPezzoRicambio(RicambioRequest);

                if (Ricambio.Amount == 0 && Ricambio.ITDescription == "" && Ricambio.RUDescription == "")
                    throw new Exception("ErroreRicambio");

                await RicambioChanged.InvokeAsync(Ricambio);
                await OnSave.InvokeAsync();
            }
            catch (Exception ex)
            {
                IsSubmitting = false;
                ErrorMessage = localizer[ex.Message];
            }
        }

        public void ResetError()
        {
            ErrorMessage = "";
        }
    }
}