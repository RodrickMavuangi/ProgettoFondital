using Fondital.Client.Utils;
using Fondital.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class DifettoClient
    {
        private readonly HttpClient httpClient;

        public DifettoClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<DifettoDto>> GetAllDifetti(bool? isAbilitato = null) =>
            await httpClient.GetFromJsonAsync<IEnumerable<DifettoDto>>($"difettiControl?isEnabled={isAbilitato}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateDifetto(int difettoId, DifettoDto difettoDto)
        {
            var response = await httpClient.PostAsJsonAsync($"difettiControl/update/{difettoId}", difettoDto, JsonSerializerOpts.JsonOpts);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateDifetto(DifettoDto difettoDto)
        {
            var response = await httpClient.PostAsJsonAsync($"difettiControl", difettoDto, JsonSerializerOpts.JsonOpts);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }
}