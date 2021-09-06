using Fondital.Shared;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            await httpClient.GetFromJsonAsync<IEnumerable<Utente>>("utentiControl", JsonSerializerOpts.JsonOpts);

        public async Task<Utente> GetUtente(string username) =>
            await httpClient.GetFromJsonAsync<Utente>($"utentiControl/{username}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateUtente(int id, Utente utente)
		{
            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            await httpClient.PutAsJsonAsync($"utenti/{id}", utente, options);
        }
    }
}
