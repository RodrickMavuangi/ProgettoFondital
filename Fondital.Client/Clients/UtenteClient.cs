using Fondital.Shared;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class UtenteClient
    {
        private readonly HttpClient httpClient;

        public UtenteClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Utente>> GetUtenti() =>
            await httpClient.GetFromJsonAsync<IEnumerable<Utente>>("utentiControl");

        public async Task<Utente> GetUtente(string username) =>
            await httpClient.GetFromJsonAsync<Utente>($"utentiControl/{username}");
    }
}
