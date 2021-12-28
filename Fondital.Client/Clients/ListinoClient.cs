using Fondital.Client.Utils;
using Fondital.Shared.Dto;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ListinoDto>> GetAllListini() =>
            await httpClient.GetFromJsonAsync<IEnumerable<ListinoDto>>($"listiniControl", JsonSerializerOpts.JsonOpts);

        public async Task<ListinoDto> GetListinoById(int id) =>
            await httpClient.GetFromJsonAsync<ListinoDto>($"listiniControl/{id}", JsonSerializerOpts.JsonOpts);

        public async Task UpdateListino(int listinoId, ListinoDto listinoDto) =>
            await httpClient.PostAsJsonAsync($"listiniControl/update/{listinoId}", listinoDto, JsonSerializerOpts.JsonOpts);

        public async Task CreateListino(ListinoDto listinoDto) =>
            await httpClient.PostAsJsonAsync($"listiniControl", listinoDto, JsonSerializerOpts.JsonOpts);
    }
}