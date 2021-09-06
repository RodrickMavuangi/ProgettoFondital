using Fondital.Shared;
using Fondital.Shared.Enums;
using Fondital.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<VoceCosto>> GetAllVociCosto(bool? isAbilitato = null) =>
            await httpClient.GetFromJsonAsync<IEnumerable<VoceCosto>>($"vociCostoControl?isEnabled={isAbilitato}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateVoceCosto(int voceCostoId, VoceCosto voceCosto) =>
            await httpClient.PostAsJsonAsync($"vociCostoControl/update/{voceCostoId}", voceCosto, JsonSerializerOpts.JsonOpts);

        public async Task CreateVoceCosto(VoceCosto voceCosto) =>
            await httpClient.PostAsJsonAsync($"vociCostoControl", voceCosto, JsonSerializerOpts.JsonOpts);
    }
}
