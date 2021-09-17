using Fondital.Shared.Dto;
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

        public async Task UpdateRapporto(int rapportoId, RapportoDto rapportoDto) =>
            await httpClient.PostAsJsonAsync($"rapportiControl/update/{rapportoId}", rapportoDto, JsonSerializerOpts.JsonOpts);

        public async Task CreateRapporto(RapportoDto rapportoDto) =>
            await httpClient.PostAsJsonAsync($"rapportiControl", rapportoDto, JsonSerializerOpts.JsonOpts);
    }
}
