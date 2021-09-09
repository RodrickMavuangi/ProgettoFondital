using Fondital.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class LavorazioneClient
    {
        private readonly HttpClient httpClient;

        public LavorazioneClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Lavorazione>> GetAllLavorazioni(bool? isAbilitato = null) =>
            await httpClient.GetFromJsonAsync<IEnumerable<Lavorazione>>($"lavorazioniControl?isEnabled={isAbilitato}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateLavorazione(int lavorazioneId, Lavorazione lavorazione)
        {
            var response = await httpClient.PostAsJsonAsync($"lavorazioniControl/update/{lavorazioneId}", lavorazione, JsonSerializerOpts.JsonOpts);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateLavorazione(Lavorazione lavorazione)
        {
            var response = await httpClient.PostAsJsonAsync($"lavorazioniControl", lavorazione, JsonSerializerOpts.JsonOpts);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }
}
