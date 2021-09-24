using Fondital.Client.Shared;
using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class Index
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        [CascadingParameter]
        public ErrorHandler ErrorHandler { get; set; }
        private UtenteDto UtenteCorrente { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authState = await AuthStateTask;
                if (authState.User.Identity.IsAuthenticated)
                    UtenteCorrente = await utClient.GetUtente(authState.User.Identity.Name);
            }
            catch (Exception e)
            {
                ErrorHandler.ProcessEx(e, "Index");
                throw;
            }
        }
    }
}