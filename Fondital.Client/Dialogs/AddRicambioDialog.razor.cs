using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddRicambioDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        protected RicambioDto Ricambio { get; set; } = new();

        protected bool isSubmitting = false;
        protected string ErrorMessage = "";
        protected async Task ConfirmAsync()
        {
            await OnSave.InvokeAsync();
        }
    }
}
