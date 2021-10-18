using Fondital.Client.Utils;
using Fondital.Shared.Dto;
using System;
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

        public async Task<IEnumerable<UtenteDto>> GetUtenti(bool? isDirezione = null)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<UtenteDto>>($"utentiControl?isDirezione={isDirezione}", JsonSerializerOpts.JsonOpts);
        }

        public async Task<UtenteDto> GetUtente(string username) =>
            await httpClient.GetFromJsonAsync<UtenteDto>($"utentiControl/getsingle/{username}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateUtente(UtenteDto utente)
        {
            var response = await httpClient.PutAsJsonAsync($"utentiControl/update", utente, JsonSerializerOpts.JsonOpts);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }
}