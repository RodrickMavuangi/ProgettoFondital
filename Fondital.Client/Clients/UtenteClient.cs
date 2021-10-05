using Fondital.Shared.Dto;
using System.Collections.Generic;
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

        public async Task<IEnumerable<UtenteDto>> GetUtenti() =>
            await httpClient.GetFromJsonAsync<IEnumerable<UtenteDto>>("utentiControl", JsonSerializerOpts.JsonOpts);

        public async Task<UtenteDto> GetUtente(string username) =>
            await httpClient.GetFromJsonAsync<UtenteDto>($"utentiControl/{username}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateUtente(UtenteDto utente)
		{
            var response =  await httpClient.PutAsJsonAsync($"utentiControl/{utente.Id}", utente, JsonSerializerOpts.JsonOpts);
            response.EnsureSuccessStatusCode();
        }     
    }
}