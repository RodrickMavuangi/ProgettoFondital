using Fondital.Shared.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class VoceCostoClient
    {
        private readonly HttpClient httpClient;

        public VoceCostoClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<VoceCostoDto>> GetAllVociCosto(bool? isAbilitato = null) =>
            await httpClient.GetFromJsonAsync<IEnumerable<VoceCostoDto>>($"vociCostoControl?isEnabled={isAbilitato}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateVoceCosto(int voceCostoId, VoceCostoDto voceCostoDto) =>
            await httpClient.PostAsJsonAsync($"vociCostoControl/update/{voceCostoId}", voceCostoDto, JsonSerializerOpts.JsonOpts);

        public async Task CreateVoceCosto(VoceCostoDto voceCostoDto) =>
            await httpClient.PostAsJsonAsync($"vociCostoControl", voceCostoDto, JsonSerializerOpts.JsonOpts);
    }
}