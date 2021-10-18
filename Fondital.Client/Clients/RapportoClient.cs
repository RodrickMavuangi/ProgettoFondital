using Fondital.Client.Utils;
using Fondital.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class RapportoClient
    {
        private readonly HttpClient httpClient;

        public RapportoClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<RapportoDto>> GetAllRapporti() =>
            await httpClient.GetFromJsonAsync<IEnumerable<RapportoDto>>($"rapportiControl", JsonSerializerOpts.JsonOpts);

        public async Task<RapportoDto> GetRapportoById(int id) =>
            await httpClient.GetFromJsonAsync<RapportoDto>($"rapportiControl/{id}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateRapporto(int rapportoId, RapportoDto rapportoDto)
        {
            var response = await httpClient.PostAsJsonAsync($"rapportiControl/update/{rapportoId}", rapportoDto, JsonSerializerOpts.JsonOpts);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<int> CreateRapporto(RapportoDto rapportoDto)
        {
            var response = await httpClient.PostAsJsonAsync($"rapportiControl", rapportoDto, JsonSerializerOpts.JsonOpts);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            var result = await response.Content.ReadFromJsonAsync<int>();
            return result;
        }
    }
}