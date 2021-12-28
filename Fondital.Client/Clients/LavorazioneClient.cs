using Fondital.Client.Utils;
using Fondital.Shared.Dto;
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

        public async Task<IEnumerable<LavorazioneDto>> GetAllLavorazioni(bool? isAbilitato = null) =>
            await httpClient.GetFromJsonAsync<IEnumerable<LavorazioneDto>>($"lavorazioniControl?isEnabled={isAbilitato}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateLavorazione(int lavorazioneId, LavorazioneDto lavorazioneDto)
        {
            var response = await httpClient.PostAsJsonAsync($"lavorazioniControl/update/{lavorazioneId}", lavorazioneDto, JsonSerializerOpts.JsonOpts);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateLavorazione(LavorazioneDto lavorazione)
        {
            var response = await httpClient.PostAsJsonAsync($"lavorazioniControl", lavorazione, JsonSerializerOpts.JsonOpts);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }
}