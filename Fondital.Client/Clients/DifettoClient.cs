using Fondital.Shared.Models;
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

        public async Task<IEnumerable<Difetto>> GetAllDifetti(bool? isAbilitato = null) =>
            await httpClient.GetFromJsonAsync<IEnumerable<Difetto>>($"difettiControl?isEnabled={isAbilitato}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateDifetto(int difettoId, Difetto difetto)
        {
            var response = await httpClient.PostAsJsonAsync($"difettiControl/update/{difettoId}", difetto, JsonSerializerOpts.JsonOpts);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateDifetto(Difetto difetto)
        {
            var response = await httpClient.PostAsJsonAsync($"difettiControl", difetto, JsonSerializerOpts.JsonOpts);
            if (!response.IsSuccessStatusCode)
               throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }
}
