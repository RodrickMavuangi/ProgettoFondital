using Fondital.Shared.Models;
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

        public async Task UpdateLavorazione(int lavorazioneId, Lavorazione lavorazione) =>
            await httpClient.PostAsJsonAsync($"lavorazioniControl/update/{lavorazioneId}", lavorazione, JsonSerializerOpts.JsonOpts);

        public async Task CreateLavorazione(Lavorazione lavorazione) =>
            await httpClient.PostAsJsonAsync($"lavorazioniControl", lavorazione, JsonSerializerOpts.JsonOpts);
    }
}
