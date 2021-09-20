using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddRapportoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }
        protected RapportoDto NuovoRapporto { get; set; } = new RapportoDto();
        UtenteDto Utente { get; set; } = new UtenteDto();

        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaRapporto()
        {
            isSubmitting = true;
            ErrorMessage = "";

            var authState = await AuthStateTask;
            if (authState.User.Identity.IsAuthenticated)
                Utente = await UtenteClient.GetUtente(authState.User.Identity.Name);

            NuovoRapporto.Utente = Utente;

            try
            {
                await RapportoClient.CreateRapporto(NuovoRapporto);
                isSubmitting = false;
                await OnSave.InvokeAsync();
            }
            catch (Exception ex)
            {
                isSubmitting = false;
                ErrorMessage = Localizer[ex.Message];
            }
        }
    }
}
