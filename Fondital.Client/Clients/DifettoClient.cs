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
    public class DifettoClient
    {
        private readonly HttpClient httpClient;

        public DifettoClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Difetto>> GetAllDifetti(bool? isAbilitato = null) =>
            await httpClient.GetFromJsonAsync<IEnumerable<Difetto>>($"difettiControl?isEnabled={isAbilitato}", JsonSerializerOpts.JsonOpts);

        //public async Task<IEnumerable<Difetto>> GetDifettiByPage(int page, bool? isAbilitato = null) =>
        //    await httpClient.GetFromJsonAsync<IEnumerable<Difetto>>($"difettiControl/{page}?isEnabled={isAbilitato}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateDifetto(int difettoId, Difetto difetto) =>
            await httpClient.PostAsJsonAsync($"difettiControl/update/{difettoId}", difetto, JsonSerializerOpts.JsonOpts);

        public async Task CreateDifetto(Difetto difetto) =>
            await httpClient.PostAsJsonAsync($"difettiControl", difetto, JsonSerializerOpts.JsonOpts);
    }
}
