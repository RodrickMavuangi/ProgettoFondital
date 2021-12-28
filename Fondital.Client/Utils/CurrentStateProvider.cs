using Fondital.Client.Authentication;
using Fondital.Client.Clients;
using Fondital.Shared.Dto;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.Utils
{
    public class CurrentStateProvider
    {       
        private FonditalAuthStateProvider _authStateProvider { get; set; }
        private UtenteClient _utClient { get; set; }
        private IJSRuntime _jsRuntime { get; set; }

        public CurrentStateProvider(FonditalAuthStateProvider authStateProvider, UtenteClient utClient, IJSRuntime jsRuntime)
        {
            _authStateProvider = authStateProvider;
            _utClient = utClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<UtenteDto> GetCurrentUser()
        {
            try
            {
                var authState = await _authStateProvider.GetAuthenticationStateAsync();
                return await _utClient.GetUtente(authState.User.Identity.Name);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> GetCurrentCulture()
        {
            var js = (IJSInProcessRuntime)_jsRuntime;
            return await js.InvokeAsync<string>("blazorCulture.get");
        }
    }
}