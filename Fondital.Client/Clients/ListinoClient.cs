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
    public class ListinoClient
    {
        private readonly HttpClient httpClient;

        public ListinoClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Listino>> GetAllListini() =>
            await httpClient.GetFromJsonAsync<IEnumerable<Listino>>($"listiniControl", JsonSerializerOpts.JsonOpts);

        public async Task<Listino> GetListinoById(int id) =>
            await httpClient.GetFromJsonAsync<Listino>($"listiniControl/{id}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateListino(int listinoId, Listino listino) =>
            await httpClient.PostAsJsonAsync($"listiniControl/update/{listinoId}", listino, JsonSerializerOpts.JsonOpts);

        public async Task CreateListino(Listino listino) =>
            await httpClient.PostAsJsonAsync($"listiniControl", listino, JsonSerializerOpts.JsonOpts);
    }
}
